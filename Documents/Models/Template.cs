    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class Template
    {
        public string Name { get; set; }
        public byte SampleBytea { get; set; }

        public Template(string name,byte sampleBytea)
        {
            Name = name;
            SampleBytea = sampleBytea;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
