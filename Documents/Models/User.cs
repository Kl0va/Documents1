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

        public string Role { get; set; }

        public User(string email, string role)
        {
            Email = email;
            Role = role;
        }
    }
}
