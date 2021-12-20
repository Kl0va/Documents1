using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documents.Models;
using Flurl.Http;
using Flurl;
using Newtonsoft.Json;

namespace Documents.Moduls
{
     static class ApiWork
     {
        private static string baseUrl = "http://127.0.0.1:8080";
        private static User currentUser;

        public static void ChangeUserEmail()
        {

        }

        public static async Task<List<Document>> GetAllAdminDocuments()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/admin").AppendPathSegment("/document").GetStringAsync();

            List<Document> documents = JsonConvert.DeserializeObject<List<Document>>(response);

            return documents;
        }


        public static async Task<List<Document>> GetAllDocuments()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/admin").AppendPathSegment("/document").GetStringAsync();
            
            List<Document> documents = JsonConvert.DeserializeObject<List<Document>>(response);

            return documents;
        }

        public static async Task<List<User>> GetAllUsers()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/user").GetStringAsync();

            List<User> documents = JsonConvert.DeserializeObject<List<User>>(response);

            return documents;
        }
        
        public static async Task<List<Template>> GetAllTemplates()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/template").GetStringAsync();

            List<Template> documents = JsonConvert.DeserializeObject<List<Template>>(response);

            return documents;
        }

        public static async Task<List<Role>> GetAllRoles()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/role").GetStringAsync();

            List<Role> documents = JsonConvert.DeserializeObject<List<Role>>(response);

            return documents;
        }

        public static async void UpdateUser(User user) => await $"{baseUrl}".AppendPathSegment("/user").PutJsonAsync(user).ReceiveString();
        
        public static async void UpdateRule(Rule rule) => await $"{baseUrl}".AppendPathSegment("/rule").PutJsonAsync(rule).ReceiveString();
        

        public static async void UpdateDocument(Document document) => await $"{baseUrl}".AppendPathSegment("/document").PutJsonAsync(document).ReceiveString();
        

        public static async void UpdateRole(Role role) => await $"{baseUrl}".AppendPathSegment("/role").PutJsonAsync(role).ReceiveString();

        public static async void AddRole(Role role) => await $"{baseUrl}".AppendPathSegment("/role").PostJsonAsync(role).ReceiveString();
        


        public static void AddDocument(string name, string desc, string template, int documentRec, byte file) => 
            AddDocument(new Document(name,desc,template,currentUser.Email,documentRec,file));
        
        private static async void AddDocument(Document document) => await $"{baseUrl}".AppendPathSegment("/document").PostJsonAsync(document).ReceiveString();
        

        public static async void AddTemplate(Template template) => await $"{baseUrl}".AppendPathSegment("/template").PostJsonAsync(template).ReceiveString();
        
     }
}
