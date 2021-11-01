using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class DocumentForReconcile
    {
        public User User { get; set; }
        public DateTime TimeOfAgreement { get; set; }
        public bool Agreed { get; set; }

        public DocumentForReconcile(User user, DateTime timeOfAgreement, bool agreed)
        {
            User = user;
            TimeOfAgreement = timeOfAgreement;
            Agreed = agreed;
        }
    }
}
