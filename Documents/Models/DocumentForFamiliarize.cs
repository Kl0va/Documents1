using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class DocumentForFamiliarize
    {
        public User User { get; set; }
        public Document Document { get; set; }

        public DocumentForFamiliarize(User user, Document document)
        {
            User = user;
            Document = document;
        }
    }
}
