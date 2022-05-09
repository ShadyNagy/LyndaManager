using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{

    class FileFunc
    {

        private string Path;
        private string FileName;
        private string FullFileName;    

        public FileFunc(string path, string fileName)
        {
            this.Path = path;
            this.FileName = fileName;
            this.FullFileName = CreateFullFileName(this.Path, this.FileName);
            CreateDirectory(Path);
        }

        public FileFunc OpenAppendText(string DataLine, Boolean IsAddNewLine = false)
        {
            OpenOrCreateFile();
            AppendTextInEnd(DataLine, IsAddNewLine);

            return this;
        }

        public FileFunc CreateAddText(string Data)
        {
            OverWriteText(Data);
            return this;
        }

        public FileFunc OpenOrCreateFile()
        {
            if (!IsFileExists(this.FullFileName))
                CreateFile(this.FullFileName);

            return this;
        }

        public FileFunc AppendTextInEnd(string DataLine, Boolean IsAddNewLine = false)
        {
            using (var tw = new StreamWriter(this.FullFileName, true))
            {
                tw.WriteLine(DataLine);
                if (IsAddNewLine)
                    tw.WriteLine("");

                tw.Close();
            }

            return this;
        }

        public FileFunc OverWriteText(string Data)
        {
            using (var tw = new StreamWriter(this.FullFileName, false))
            {
                tw.WriteLine(Data);

                tw.Close();
            }

            return this;
        }      

        public FileFunc AddExtantion(string exetantion)
        {
            FileName = FileName + "." + exetantion;
            FullFileName = FullFileName + "." + exetantion;

            return this;
        }

        private string CreateFullFileName(string Path, string FileName)
        {
            return Path + @"\" + FileName;
        }

        private void CreateFile(string FullFileName)
        {
            File.Create(FullFileName).Dispose();
        }        

        private void CreateDirectory(string Path)
        {
            bool exists = System.IO.Directory.Exists(Path);

            if (!exists)
              System.IO.Directory.CreateDirectory(Path);
        }

        private Boolean IsFileExists(string FullFileName)
        {
            return File.Exists(FullFileName);
        }        
    }
}
