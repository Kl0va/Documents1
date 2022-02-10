using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class DocumentForFamiliarize
    {
        public int id { get; set; }
        public string employeeId { get; set; }
        public Models.Documents documentId { get; set; }
        public bool status { get; set; }

        public DocumentForFamiliarize(string user, Documents document,bool Status)
        {
            employeeId = user;
            documentId = document;
            status = Status;
        }
    }
}
