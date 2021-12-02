using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class Role
    {
        public string Name { get; set; }
        
        public Role(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}
