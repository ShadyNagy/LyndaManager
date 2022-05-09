using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WpfApplication1;

namespace WpfApplication1
{
    class ConfigFunc
    {

        public static Settings ReadSettings()
        {
            Settings settings = new Settings();

            string fileName = @"settings.json";
            if (!File.Exists(fileName))
            {
                return settings;

            }

            string text;
            var fileStream = new FileStream(@"settings.json", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
                streamReader.Close();
            }


            dynamic jObj = JsonConvert.DeserializeObject(text);
            settings.User = jObj.User;
            settings.Password = jObj.Password;
            settings.LoginLink = jObj.LoginLink;
            settings.CourseLink = jObj.CourseLink;
            settings.ChkSave = jObj.ChkSave;

            return settings;
        }

        public static void SaveAllSettings(Settings setting)
        {
            Settings settingClone = new Settings();

            if (setting.ChkSave)
            {
                settingClone.User = setting.User;
                settingClone.Password = setting.Password;
                settingClone.LoginLink = setting.LoginLink;
            }
            settingClone.CourseLink = setting.CourseLink;
            settingClone.ChkSave = setting.ChkSave;

            dynamic stuff1 = settingClone;
            string jsonData = JsonConvert.SerializeObject(stuff1);

            string fileName = @"settings.json";
            if (!File.Exists(fileName))
            {

                File.Create(fileName).Dispose();
                using (TextWriter tw = new StreamWriter(fileName))
                {
                    tw.Write(jsonData);
                    tw.Close();
                }
            }
            else if (File.Exists(fileName))
            {
                using (var tw = new StreamWriter(fileName, false))
                {
                    tw.Write(jsonData);
                    tw.Close();
                }
            }
        }

        public static void SaveCourse(List<Course> courses)
        {

            dynamic stuff1 = courses;
            string jsonData = JsonConvert.SerializeObject(stuff1);

            string fileName = @"courses.json";
            if (!File.Exists(fileName))
            {

                File.Create(fileName).Dispose();
                using (TextWriter tw = new StreamWriter(fileName))
                {
                    tw.Write(jsonData);
                    tw.Close();
                }
            }
            else if (File.Exists(fileName))
            {
                using (var tw = new StreamWriter(fileName, false))
                {
                    tw.Write(jsonData);
                    tw.Close();
                }
            }
        }

        public static List<Course> LoadCourses()
        {
            List<Course> courses = new List<Course>();

            string fileName = @"courses.json";
            if (!File.Exists(fileName))
            {
                return courses;

            }

            string text;
            var fileStream = new FileStream(@"courses.json", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
                streamReader.Close();
            }


            dynamic jObj = JsonConvert.DeserializeObject(text);

            foreach (var package in jObj)
            {
                Course course = new Course();
                course.CourseName = package.CourseName;
                course.IsDownloaded = package.IsDownloaded;
                course.CourseLink = package.CourseLink;
                courses.Add(course);
            }


            return courses;
        }
    }
}      
