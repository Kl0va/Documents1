using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class Rule
    {
        public bool Add { get; set; }
        public bool Check { get; set; }
        public bool Send { get; set; }
        public bool Reconcile { get; set; }
        public bool Shape { get; set; }
        public bool Familiarize { get; set; }

        public Rule(bool add, bool check, bool send, bool reconcile, bool shape, bool familiarize)
        {
            Add = add;
            Check = check;
            Send = send;
            Reconcile = reconcile;
            Shape = shape;
            Familiarize = familiarize;
        }
    }
}
