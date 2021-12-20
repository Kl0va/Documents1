using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class Role
    {
        public string name { get; set; }
        
        public Role(string Name)
        {
            name = Name;
        }

        public override string ToString() => name;
    }
}
