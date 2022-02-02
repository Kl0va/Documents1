    using Documents.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents
{
    class User
    {
        public string id { get; set; }
        public string Email { get; set;}
        public int Rule { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }

        [JsonConstructor]
        public User(string email, string firstName,string secondName,string middleName, int rule,string role)
        {
            Email = email;
            FirstName = firstName;
            SecondName = secondName;
            MiddleName = middleName;
            Rule = rule;
            Role = role;
        }
        public User(string role)
        {
            Role = role;
        }
    }
}
