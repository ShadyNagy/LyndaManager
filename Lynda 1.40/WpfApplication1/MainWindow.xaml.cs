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

            Thread thread = new Thread(AllTh[Jobe]);
            thread.Start();
        }

        public static void DoneWithMessage(string txt)
        {
            if(txt != null)
                main.AddTextOut(txt);            
            main.EnableButtons();
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
                    new Log().AddLogText(txt);
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

        public void AddTextOut(string data)
        {
            Thread thread = new Thread(() => AddTextThread(data));
            thread.Start();
        }

        private void AddTextThread(string data)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Output.AppendText(data);
                Output.AppendText(Environment.NewLine);
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
                DoneWithMessage("Error, Check Login Info!!!!");
                return;
            }

            lynda.GetCourseExercise(settings.CourseLink);          

            sw.Stop();
            var time = sw.Elapsed;
            DoneWithMessage("All Exercises Downloaded Done --> " + time);
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
                DoneWithMessage("Error, Check Login Info!!!!");
                return;
            }

            for (int i = 0; i < MainWindow.courses.Count; i++)
            {
                if (!MainWindow.courses[i].IsDownloaded)
                {
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
            DoneWithMessage("All Courses Downloaded Done --> " + time);
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
                DoneWithMessage("Error, Check Login Info!!!!");
                return;
            }

            var dialog = new MyDialog();
            string CourseName = "";
            if (dialog.ShowDialog() == true)
            {
                if (IsValiedCourseLink(dialog.ResponseText))
                {
                    CourseName = new Lynda().GetCourseTitleOnly(dialog.ResponseText);
                    if (CourseName != "")
                    {
                        Course course = new Course() { CourseName = CourseName, IsDownloaded = false, CourseLink = dialog.ResponseText };
                        courses.Add(course);
                        ConfigFunc.SaveCourse(courses);
                        LvLoadRefresh();
                    }
                    else
                    {
                        DoneWithMessage(CourseName + " Add Error");
                        return;
                    }
                }
            }

            DoneWithMessage(CourseName + " Added");
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
                DoneWithMessage("Error, Check Login Info!!!!");
                return;
            }            

            lynda.GetCourseSub(settings.CourseLink);

            sw.Stop();
            var time = sw.Elapsed;
            DoneWithMessage("Get Subtitles Done --> " + time);
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
                DoneWithMessage("Error, Check Login Info!!!!");
                return;
            }

            lynda.GetCourseVideos(settings.CourseLink);

            sw.Stop();
            var time = sw.Elapsed;
            DoneWithMessage("Get Videos Done --> " + time);
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
                DoneWithMessage("Bad Course Link");
                return false;
            }

            if (!IsLoginInfoShowMessage())
            {
                DoneWithMessage("Error");
                return false;
            }

            if (!IsCourseLinkShowMessage())
            {
                DoneWithMessage("Error");
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
