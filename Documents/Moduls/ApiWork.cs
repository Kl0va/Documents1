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
        private static string baseUrl = "http://0.0.0.0:8080";


        public static async Task<List<Document>> GetAllDocuments(int personId)
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/documents/all").GetStringAsync();
            
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
            var response = await $@"{baseUrl}".AppendPathSegment("/roles").GetStringAsync();

            List<Role> documents = JsonConvert.DeserializeObject<List<Role>>(response);

            return documents;
        }

        public static async void UpdateUser(User user) 
        {
            var response = await $"{baseUrl}".AppendPathSegment("/update_user").PutJsonAsync(user);
        }

        public static async void UpdateDocument(Document document)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/update_document").PutJsonAsync(document);
        }

        public static async void UpdateRole(Role role)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/update_role").PutJsonAsync(role);
        }

        public static async void AddRole(Role role)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/create_role").PostJsonAsync(role);
        }

        public static async void AddDocument(Document document)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/create_document").PostJsonAsync(document);
        }

        public static async void AddTemplate(Template template)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/create_template").PostJsonAsync(template);
        }
     }
}
