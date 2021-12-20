using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class Restriction
    {
        public string typeName { get; set; }
        public bool delete { get; set; }
        public bool check { get; set; }
        public bool change { get; set; }
        public string role { get; set; }

        public Restriction(string template, bool Delete,bool Check,bool Change,string Role)
        {
            typeName = template;
            delete = Delete;
            check = Check;
            change = Change;
            role = Role;
        }
    }
}
