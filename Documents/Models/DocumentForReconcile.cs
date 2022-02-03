using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class DocumentForReconcile
    {
        public int id { get; set; }
        public string employeeId { get; set; }
        public DateTime timeOfAgreement { get; set; }
        public bool agreed { get; set; }
        public int documentId { get; set; }

        public DocumentForReconcile(string user, DateTime TimeOfAgreement, bool Agreed,int Document)
        {
            employeeId = user;
            timeOfAgreement = TimeOfAgreement;
            agreed = Agreed;
            documentId = Document;
        }
    }
}
