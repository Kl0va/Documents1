    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class Template
    {
        public string name { get; set; }
        public string sampleByte { get; set; }

        public Template(string Name,string SampleBytea)
        {
            name = Name;
            sampleByte = SampleBytea;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
