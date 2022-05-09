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

        public void AddLog(string LogData)
        {
            FileFunc fileFunc = new FileFunc("logs", GetDate())
                .AddExtantion("log")
                .OpenAppendText(CreateLogWithTime(LogData), true);
        }

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

        

        private string CreateLogWithTime(string LogData)
        {
            return GetTime() + " -----> " + LogData;
        }        
    }
}
