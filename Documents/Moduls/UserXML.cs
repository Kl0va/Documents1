using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Documents.Moduls
{
    [Serializable]
    class UserXML
    {
        public static void SerializeUser(User user)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(User));
            using (FileStream fs = new FileStream("user.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, user);
            }
        }
        
        public static User DeserializeUser()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(User));
            using (FileStream fs = new FileStream("user.xml", FileMode.OpenOrCreate))
            {
                User user = (User)formatter.Deserialize(fs);
                return user;
            }
        }
    }
}
