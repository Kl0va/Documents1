using Documents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents
{
    class User
    {
        public string Email { get; set;}

        //public Role Role { get; set; }
        //public Rule Rule { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }


        public User(string email, string firstName,string secondName,string middleName)
        {
            Email = email;
            //Role = role;
            //Rule = rule;
            FirstName = firstName;
            SecondName = secondName;
            MiddleName = middleName;
        }
    }
}
