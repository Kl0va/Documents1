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
        public int documentId { get; set; }

        public DocumentForFamiliarize(string user, int document)
        {
            employeeId = user;
            documentId = document;
        }
    }
}
