using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class Restriction
    {
        public Template Template { get; set; }
        public bool Delete { get; set; }
        public bool Check { get; set; }
        public bool Change { get; set; }
        public Role Role { get; set; }

        public Restriction(Template template, bool delete,bool check,bool change,Role role)
        {
            Template = template;
            Delete = delete;
            Check = check;
            Change = change;
            Role = role;
        }
    }
}
