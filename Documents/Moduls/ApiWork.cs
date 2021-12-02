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

        public static async Task<List<Document>> GetAllDocuments(int personId)
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/documents").GetStringAsync();
            
            List<Document> documents = JsonConvert.DeserializeObject<List<Document>>(response);

            return documents;
        }

        public static async Task<List<User>> GetAllUsers()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/users").GetStringAsync();

            List<User> documents = JsonConvert.DeserializeObject<List<User>>(response);

            return documents;
        }
        
        public static async Task<List<Template>> GetAllTemplates()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/templates").GetStringAsync();

            List<Template> documents = JsonConvert.DeserializeObject<List<Template>>(response);

            return documents;
        }

        public static async Task<List<Role>> GetAllRoles()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/role").GetStringAsync();

            List<Role> documents = JsonConvert.DeserializeObject<List<Role>>(response);

            return documents;
        }

        public static async void UpdateUser(User user) 
        {
            var response = await $"{baseUrl}".AppendPathSegment("/user").PutJsonAsync(user).ReceiveString();
        }

        public static async void UpdateDocument(Document document)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/document").PutJsonAsync(document).ReceiveString();
        }

        public static async void UpdateRole(Role role)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/role").PutJsonAsync(role).ReceiveString();
        }

        public static async void AddRole(Role role)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/role").PostJsonAsync(role).ReceiveString();
        }


        public static void AddDocument(string name, string desc, string template, int documentRec, byte file) => 
            AddDocument(new Document(name,desc,template,currentUser.Email,documentRec,file));
        
        private static async void AddDocument(Document document)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/create_document").PostJsonAsync(document).ReceiveString();
        }

        public static async void AddTemplate(Template template)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/create_template").PostJsonAsync(template).ReceiveString();
        }
     }
}
