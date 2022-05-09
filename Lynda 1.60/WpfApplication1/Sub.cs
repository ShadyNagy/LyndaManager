using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Sub
    {
        private List<SubField> sub;
        private FileFunc SRTFile;

        public Sub()
        {
            sub = new List<SubField>();            
        }

        public void CreateSubFile(string CourseName, string FileName)
        {
            SRTFile = new FileFunc(CourseName, FileName);
            SRTFile.AddExtantion("srt")
                .GetFullName();
            

            if (!File.Exists(pathString))
            {

                File.Create(path).Dispose();
                using (TextWriter tw = new StreamWriter(path))
                {
                    for (int i = 0; i < Sub.Count; i++)
                    {
                        tw.WriteLine(Sub[i].id);
                        tw.WriteLine(Sub[i].start + " --> " + subs[i].end);
                        tw.WriteLine(Sub[i].data);
                        tw.WriteLine("");
                    }
                    tw.Close();
                }
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, false))
                {
                    for (int i = 0; i < subs.Count; i++)
                    {
                        tw.WriteLine(subs[i].id);
                        tw.WriteLine(subs[i].start + " --> " + subs[i].end);
                        tw.WriteLine(subs[i].data);
                        tw.WriteLine("");
                    }
                    tw.Close();
                }
            }
        }

        public int Count()
        {
            return sub.Count();
        }

        public SubField GetSubField(int Index)
        {
            return sub[Index];
        }

        public void AddSubField(SubField subField)
        {
            sub.Add(subField);
        }
    }
}
