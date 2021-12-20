using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class Restriction
    {
        public Template typeName { get; set; }
        public bool delete { get; set; }
        public bool check { get; set; }
        public bool change { get; set; }
        public Role role { get; set; }

        public Restriction(Template template, bool Delete,bool Check,bool Change,Role Role)
        {
            typeName = template;
            delete = Delete;
            check = Check;
            change = Change;
            role = Role;
        }
    }
}
