using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Log
    {

        private string GetTime()
        {
            return DateTime.Now.ToString("h:mm:ss tt");
        }

        private string GetDate()
        {
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

            String dy = datevalue.Day.ToString();
            String mn = datevalue.Month.ToString();
            String yy = datevalue.Year.ToString();

            return dy + "-" + mn + "-" + yy;
        }

        private string CreateLogFileName()
        {
            string pathString = System.IO.Path.Combine("logs");
            System.IO.Directory.CreateDirectory(pathString);
            string path = pathString + @"\";

            return path + GetDate() + ".log";
        }

        private Boolean IsLogExist(string LogName)
        {           
            return File.Exists(LogName);
        }

        private void CreateLogFile(string FileName)
        {
            File.Create(FileName).Dispose();
        }

        public void AddLogText(string txt)
        {
            string LogName = CreateLogFileName();
            Boolean isLog = IsLogExist(LogName);

            if (!isLog)
            {
                CreateLogFile(LogName);
            }

            AddTextToLog(txt, LogName, isLog);
        }

        private void AddTextToLog(string txt, string LogName, Boolean isLog)
        {
            string TimeStr = GetTime();

            if (!isLog)
            {
                using (TextWriter tw = new StreamWriter(LogName))
                {
                    tw.WriteLine(TimeStr + " -----> ");
                    tw.WriteLine(txt);
                    tw.WriteLine("");
                    tw.Close();
                }
            }
            else
            {
                using (var tw = new StreamWriter(LogName, true))
                {
                    tw.WriteLine(TimeStr + " -----> ");
                    tw.WriteLine(txt);
                    tw.WriteLine("");
                    tw.Close();
                }
            }
        }        
    }
}
