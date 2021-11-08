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
        public string User { get; set; }
        public string Type { get; set; }
        public int DocumentRec { get; set; }
        public byte File { get; set; }
       
        public Document(string name, string description, string type, string user, int documentRec, byte file)
        {
            Name = name;
            Description = description;
            Type = type;
            User = user;
            DocumentRec = documentRec;
            File = file;
        }
    }
}
