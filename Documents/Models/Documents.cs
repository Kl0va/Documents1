using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class Documents
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string employeeId { get; set; }
        public string typeId { get; set; }
        public string documentsByte { get; set; }
       
        public Documents(string Name, string Description, string Type, string User, string File)
        {
            name = Name;
            description = Description;
            typeId = Type;
            employeeId = User;
            documentsByte = File;
        }
        [JsonConstructor]
        public Documents(string Name,string Type,string User,string File)
        {
            name = Name;
            typeId = Type;
            employeeId = User;
            documentsByte = File;
        }
    }
}
