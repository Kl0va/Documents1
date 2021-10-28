using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class Document
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Template Type { get; set; }

        public byte File { get; set; }
       
        public Document(string name, string description, Template type,byte file)
        {
            Name = name;
            Description = description;
            Type = type;
            File = file;
        }
    }
}
