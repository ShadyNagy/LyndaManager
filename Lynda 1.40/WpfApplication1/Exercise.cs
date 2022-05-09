using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    

    class Exercise
    {
        public string Name { get;  set; }
        public string Link { get; set; }
        public string Size { get; set; }

        public Exercise()
        {

        }

        public Exercise(string Name, string Size, string Link)
        {
            this.Name = Name;
            this.Size = Size;
            this.Link = Link;
        }
    }
}
