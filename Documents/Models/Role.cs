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
        public int Count { get; set; }
        
        public Role(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}
