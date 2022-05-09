using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication1;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static Settings settings;

        private static Boolean DownloadNeedExercises;


        private static int Jobe_DownloadAll;
        private static int Jobe_GetSubtitles;
        private static int Jobe_GetCourseVideos;
        public static int Jobe_DownloadExercises;

        private static List<ThreadStart> AllTh;

        public static Boolean Stop;
        private static List<Course> courses;
        private Lynda lynda;
        internal static MainWindow main;

        private void InitilaizeStartData()
        {
            main = this;

            settings = new Settings();

            DownloadNeedExercises = false;

            Jobe_DownloadAll =0;
            Jobe_GetSubtitles=1;
            Jobe_GetCourseVideos=2;
            Jobe_DownloadExercises = 3;

            AllTh = new List<ThreadStart>();
            AllTh.Add(DownloadAllTh);
            AllTh.Add(GetSubtitlesTh);
            AllTh.Add(GetCourseVideosTh);
            AllTh.Add(DownloadExercisesTh);


            MainWindow.courses = new List<Course>();
            MainWindow.courses = ConfigFunc.LoadCourses();
            LvLoadRefresh();

            MainWindow.Stop = false;

            settings.User = "";
            settings.Password = "";
            settings.LoginLink = "";
            settings.CourseLink = "";
            settings.ChkSave = false;

            LoginInfo.ReceivedCookies = "";
            LoginInfo.SendCookies = "";
            LoginInfo.IsLogin = false;
            lynda = new Lynda();

            MainWindow.settings = ConfigFunc.ReadSettings();
            txtUser.Text = settings.User;
            txtPass.Password = settings.Password;
            txtLibraryLink.Text = settings.LoginLink;
            txtCourseLink.Text = settings.CourseLink;
            chkSave.IsChecked = settings.ChkSave;
        }


        public MainWindow()
        {
            InitializeComponent();
            InitilaizeStartData();
        }

        public void JobThreadMain(int Jobe)
        {
            ResetProgressBar();

            Thread thread = new Thread(AllTh[Jobe]);
            thread.Start();
        }

        public void DoneWithErrorMessage(string txt)
        {
            if(txt != null)
                main.AddTextOut(txt, "ff0000");            
            main.EnableButtons();
        }

        public void DoneWithSuccessMessage(string txt)
        {
            if (txt != null)
                main.AddTextOut(txt, "00ff00");
            main.EnableButtons();
        }

        public void SetSpeed(int ReadedBytes, double Seconds)
        {
            Thread thread = new Thread(() => SetSpeedTh(ReadedBytes, Seconds));
            thread.Start();            
        }

        public void SetSpeedTh(int ReadedBytes, double Seconds)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if ((ReadedBytes > 0) && (Seconds > 0))
                    MainWindow.main.lblSpeed.Content = string.Format("Speed: {0} kb/s", (ReadedBytes / 1024d / Seconds).ToString("0.00"));
            }));
            
        }

        

        public void AddLog(string txt)
        {
            Thread thread = new Thread(() => AddLogTh(txt));
            thread.Start();
        }

        private void AddLogTh(string txt)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (txt != null)
                    new Log().AddLog(txt);
            }));

            
        }


        private void LvLoadRefresh()
        {            
            Thread thread = new Thread(LvLoadRefreshTh);
            thread.Start();
        }

        private void LvLoadRefreshTh()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lvCourses.ItemsSource = courses;
                ICollectionView view = CollectionViewSource.GetDefaultView(lvCourses.ItemsSource);
                if (view != null)
                    view.Refresh();
            }));            
        }

        public void DisableButtons()
        {
            Thread thread = new Thread(DisableButtonsThread);
            thread.Start();
        }

        private void DisableButtonsThread()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                btnGetSubtitles.IsEnabled = false;
                btnGetExercises.IsEnabled = false;
                btnGetVideos.IsEnabled = false;
                //btnAddCourse.IsEnabled = false;
                btnDownloadAll.IsEnabled = false;
                //btnRemoveCourses.IsEnabled = false;
            }));
        }


        public void EnableButtons()
        {
            Thread thread = new Thread(EnableButtonsThread);
            thread.Start();
        }

        private void EnableButtonsThread()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                btnGetSubtitles.IsEnabled = true;
                btnGetExercises.IsEnabled = true;
                btnGetVideos.IsEnabled = true;
                btnAddCourse.IsEnabled = true;
                btnDownloadAll.IsEnabled = true;
                btnRemoveCourses.IsEnabled = true;
            }));
        }        

        public void ResetProgressBar()
        {
            SetTxtPbSmallStatus(0, 0);
            SetTxtPbStatus("");

            SetPbSmall(0);
            SetProgress(0);
        }

        public void SetTxtPbSmallStatus(int VideoSize, int bytesReaded)
        {
            Thread thread = new Thread(() => SetTxtPbSmallStatusThread(VideoSize, bytesReaded));
            thread.Start();
        }

        private void SetTxtPbSmallStatusThread(int VideoSize, int bytesReaded)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                txtPbSmallStatus.Text = string.Format("{0} MB's / {1} MB's",
                    (bytesReaded / 1024d / 1024d).ToString("0.00"),
                    (VideoSize / 1024d / 1024d).ToString("0.00"));
            }));
        }

        public void SetTxtPbStatus(string val)
        {
            Thread thread = new Thread(() => SetTxtPbStatusThread(val));
            thread.Start();
        }

        private void SetTxtPbStatusThread(string val)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                txtPbStatus.Text = val;
            }));
        }


        public void SetMaxPbSmallStatus(int val)
        {
            Thread thread = new Thread(() => SetMaxPbSmallStatusThread(val));
            thread.Start();
        }

        private void SetMaxPbSmallStatusThread(int val)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                pbSmallStatus.Maximum = val;
            }));
        }

        public void SetPbSmall(int val)
        {
            Thread thread = new Thread(() => SetPbSmallThread(val));
            thread.Start();
        }

        private void SetPbSmallThread(int val)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                pbSmallStatus.Value = val;
            }));
        }



        public void SetMaxProgress(int val)
        {
            Thread thread = new Thread(() => SetMaxProgressThread(val));
            thread.Start();
        }

        private void SetMaxProgressThread(int val)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                pbStatus.Maximum = val;
            }));
        }

        public void SetProgress(int val)
        {
            Thread thread = new Thread(() => SetProgressThread(val));
            thread.Start();
        }

        private void SetProgressThread(int val)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                pbStatus.Value = val;
            }));
        }

        private void myAppendText(RichTextBox box, string text, string color, Boolean NewLine)
        {
            BrushConverter bc = new BrushConverter();
            TextRange tr = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd);
            if (NewLine)
                tr.Text = text + "\n";
            else
                tr.Text = text;
            try
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, bc.ConvertFromString("#" + color));
            }
            catch (FormatException ex)
            {
                MainWindow.main.AddLog(ex.ToString());
            }
        }

        public void AddTextOut(string data, string color="ffffff", Boolean NewLine=true)
        {
            Thread thread = new Thread(() => AddTextThread(data, color, NewLine));
            thread.Start();
        }

        private void AddTextThread(string data, string color="ffffff", Boolean NewLine=true)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                main.myAppendText(Output, data, color, NewLine);
                Output.ScrollToEnd();
            }));
        }

        private Boolean CheckLoginTextBox()
        {
            if ((txtUser.Text != "") && (txtPass.Password != "") && (txtLibraryLink.Text != ""))
                return true;

            return false;
        }

        private Boolean IsLoginInfoShowMessage()
        {
            if (!CheckLoginTextBox())
            {
                MessageBox.Show("Please Enter Your Card Number, Pin Number and Library Link", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private Boolean CheckCourseLinkTextBox()
        {
            if (txtCourseLink.Text != "")
                return true;

            return false;
        }

        private Boolean IsCourseLinkShowMessage()
        {
            if (!CheckCourseLinkTextBox())
            {
                MessageBox.Show("Please Enter Full Course Link", "Course Link Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        public void DownloadExercises()
        {
            JobThreadMain(Jobe_DownloadExercises);
        }

        private void DownloadExercisesTh()
        {
            Stopwatch sw = Stopwatch.StartNew();

            lynda.GetLoginPageHTMLAndCokies(settings.LoginLink);
            lynda.DoLoginUserPass(settings.User, settings.Password, settings.LoginLink);

            LoginInfo.IsLogin = lynda.IsLogedIn();

            if (LoginInfo.IsLogin)
            {
                AddTextOut("Login Success");
            }
            else
            {
                DoneWithErrorMessage("Error, Check Login Info!!!!");
                return;
            }

            lynda.GetCourseExercise(settings.CourseLink);          

            sw.Stop();
            var time = sw.Elapsed;
            DoneWithSuccessMessage("All Exercises Downloaded Done --> " + time);
        }

        public void DownloadAll()
        {
            JobThreadMain(Jobe_DownloadAll);
        }
        

        private void DownloadAllTh()
        {
            Stopwatch sw = Stopwatch.StartNew();

            lynda.GetLoginPageHTMLAndCokies(settings.LoginLink);
            lynda.DoLoginUserPass(settings.User, settings.Password, settings.LoginLink);

            LoginInfo.IsLogin = lynda.IsLogedIn();

            if (LoginInfo.IsLogin)
            {
                AddTextOut("Login Success");
            }
            else
            {
                DoneWithErrorMessage("Error, Check Login Info!!!!");
                return;
            }

            for (int i = 0; i < MainWindow.courses.Count; i++)
            {
                if (!MainWindow.courses[i].IsDownloaded)
                {
                    SelectItemDownloading(i);                    
                    lynda.GetCourseSub(MainWindow.courses[i].CourseLink);

                    if(MainWindow.DownloadNeedExercises)
                        lynda.GetCourseExercise(MainWindow.courses[i].CourseLink);

                    lynda.GetCourseVideos(MainWindow.courses[i].CourseLink);
                    if (!Stop)
                    {
                        MainWindow.courses[i].IsDownloaded = true;
                        ConfigFunc.SaveCourse(courses);
                        LvLoadRefresh();
                    }
                }
            }

            sw.Stop();
            var time = sw.Elapsed;
            DoneWithSuccessMessage("All Courses Downloaded Done --> " + time);
        }

        public void AddCourse()
        {
            Thread thread = new Thread(AddCourseTh);
            thread.Start();
        }

        private void AddCourseTh()
        {
            lynda.GetLoginPageHTMLAndCokies(settings.LoginLink);
            lynda.DoLoginUserPass(settings.User, settings.Password, settings.LoginLink);

            LoginInfo.IsLogin = lynda.IsLogedIn();

            if (LoginInfo.IsLogin)
            {
                AddTextOut("Login Success");
            }
            else
            {
                DoneWithErrorMessage("Error, Check Login Info!!!!");
                return;
            }

            string ToAddLink = "";
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                var dialog = new MyDialog();
                if (dialog.ShowDialog() == true)
                { 
                    ToAddLink = dialog.ResponseText;

                    string CourseName = "";
                    if (IsValiedCourseLink(ToAddLink))
                    {
                        CourseName = new Lynda().GetCourseTitleOnly(ToAddLink);
                        if (CourseName != "")
                        {
                            Course course = new Course() { CourseName = CourseName, IsDownloaded = false, CourseLink = ToAddLink };
                            courses.Add(course);
                            ConfigFunc.SaveCourse(courses);
                            LvLoadRefresh();
                        }
                        else
                        {
                            DoneWithErrorMessage(CourseName + " Add Error");
                            return;
                        }

                    }

                    DoneWithSuccessMessage(CourseName + " Added");
                }
            }));            
        }

        private void GetSubtitles()
        {
            JobThreadMain(Jobe_GetSubtitles);
        }

        private void GetSubtitlesTh()
        {
            Stopwatch sw = Stopwatch.StartNew();

            lynda.GetLoginPageHTMLAndCokies(settings.LoginLink);
            lynda.DoLoginUserPass(settings.User, settings.Password, settings.LoginLink);

            LoginInfo.IsLogin = lynda.IsLogedIn();

            if (LoginInfo.IsLogin)
            {
                AddTextOut("Login Success");
            }
            else
            {
                DoneWithErrorMessage("Error, Check Login Info!!!!");
                return;
            }            

            lynda.GetCourseSub(settings.CourseLink);

            sw.Stop();
            var time = sw.Elapsed;
            DoneWithSuccessMessage("Get Subtitles Done --> " + time);
        }

        private void GetVideos()
        {
            JobThreadMain(Jobe_GetCourseVideos);
        }

        private void GetCourseVideosTh()
        {
            Stopwatch sw = Stopwatch.StartNew();

            lynda.GetLoginPageHTMLAndCokies(settings.LoginLink);
            lynda.DoLoginUserPass(settings.User, settings.Password, settings.LoginLink);

            LoginInfo.IsLogin = lynda.IsLogedIn();

            if (LoginInfo.IsLogin)
            {
                AddTextOut("Login Success");
            }
            else
            {
                DoneWithErrorMessage("Error, Check Login Info!!!!");
                return;
            }

            lynda.GetCourseVideos(settings.CourseLink);

            sw.Stop();
            var time = sw.Elapsed;
            DoneWithSuccessMessage("Get Videos Done --> " + time);
        }

        private Boolean IsValiedCourseLink(string CourseLink)
        {
            if (CourseLink.IndexOf("Welcome") > 0)
                return false;

            return true;
        }

        private Boolean CheckCourseLinkFullAndLogin(){

            if (!IsValiedCourseLink(txtCourseLink.Text))
            {
                DoneWithErrorMessage("Bad Course Link");
                return false;
            }

            if (!IsLoginInfoShowMessage())
            {
                DoneWithErrorMessage("Error");
                return false;
            }

            if (!IsCourseLinkShowMessage())
            {
                DoneWithErrorMessage("Error");
                return false;
            }

            return true;
        }
        
        private void btnGetSubtitles_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Stop = false;

            LoadUISettings();
            SaveUISettings();

            if (!CheckCourseLinkFullAndLogin())
                return;

            DisableButtons();
            AddTextThread("Please Wait...");            

            GetSubtitles();            
        }

        private void LoadUISettings()
        {
            settings.User = txtUser.Text;
            settings.Password = txtPass.Password;
            settings.LoginLink = txtLibraryLink.Text;
            settings.CourseLink = txtCourseLink.Text;
            settings.ChkSave = chkSave.IsChecked.Value;       
        }

        private void SaveUISettings()
        {
            ConfigFunc.SaveAllSettings(settings);
        }

        private void btnDownloadAll_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Stop = false;

            if (MessageBox.Show("You Need Exercises?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                DownloadNeedExercises = false;
            }
            else
            {
                DownloadNeedExercises = true;
                //do yes stuff
            }

            LoadUISettings();
            SaveUISettings();

            if (!CheckCourseLinkFullAndLogin())
                return;

            DisableButtons();
            AddTextThread("Please Wait...");

            DownloadAll();            
        }


        private void btnGetExercises_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Stop = false;

            LoadUISettings();
            SaveUISettings();

            if (!CheckCourseLinkFullAndLogin())
                return;

            DisableButtons();
            AddTextThread("Please Wait...");

            DownloadExercises();
        }

        private void btnRemoveCourse_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Stop = false;

            if (lvCourses.SelectedItems.Count > 0)
            {
                int selectedIndex = lvCourses.Items.IndexOf(lvCourses.SelectedItems[0]);
                courses.RemoveAt(selectedIndex);
                ConfigFunc.SaveCourse(courses);
                LvLoadRefresh();
            }           
        }

        public void SelectItemDownloading(int Index)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lvCourses.SelectedItems.Clear();
                lvCourses.SelectedItems.Add(lvCourses.Items.GetItemAt(Index));                    
            }));
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = this.lvCourses.SelectedIndex;

            if (selectedIndex > 0)
            {
                var itemToMoveUp = courses[selectedIndex];
                courses.RemoveAt(selectedIndex);
                courses.Insert(selectedIndex - 1, itemToMoveUp);
                lvCourses.SelectedIndex = selectedIndex - 1;
            }

            ConfigFunc.SaveCourse(courses);
            LvLoadRefresh();
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = this.lvCourses.SelectedIndex;

            if (selectedIndex + 1 < courses.Count)
            {
                var itemToMoveDown = courses[selectedIndex];
                courses.RemoveAt(selectedIndex);
                courses.Insert(selectedIndex + 1, itemToMoveDown);
                lvCourses.SelectedIndex = selectedIndex + 1;
            }

            ConfigFunc.SaveCourse(courses);
            LvLoadRefresh();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {           
            AddTextOut("Wait It Will Stop Soon...");
            MainWindow.Stop = true;
        }

        private void btnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Stop = false;

            LoadUISettings();
            SaveUISettings();

            DisableButtons();
            AddTextThread("Please Wait...");

            AddCourse();
        }

        private void btnGetVideos_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Stop = false;

            LoadUISettings();
            SaveUISettings();

            if (!CheckCourseLinkFullAndLogin())
                return;

            DisableButtons();
            AddTextThread("Please Wait...");            

            GetVideos();
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            LoadUISettings();
            SaveUISettings();
        }
    }
}
