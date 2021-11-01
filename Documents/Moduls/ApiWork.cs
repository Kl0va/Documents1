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
        private static string baseUrl = "http:/";


        public static Task<List<Document>> GetAllDocuments(int personId)
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/documents").GetStringAsync();
            
            List<Document> documents = JsonConvert.DeserializeObject<List<Document>>(response);

            return documents;
        }

        public static Task<List<User>> GetAllUsers()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/users").GetStringAsync();

            List<User> documents = JsonConvert.DeserializeObject<List<User>>(response);

            return documents;
        }
        
        public static Task<List<Template>> GetAllTemplates()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/templates").GetStringAsync();

            List<Template> documents = JsonConvert.DeserializeObject<List<Template>>(response);

            return documents;
        }

        public static Task<List<Role>> GetAllRoles()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/roles").GetStringAsync();

            List<Role> documents = JsonConvert.DeserializeObject<List<Role>>(response);

            return documents;
        }

        public async void UpdateUser(string newEmail,string newFullName, string newRole) 
        {
            var response = await $"{baseUrl}".AppendPathSegment("/update_user").PutJsonAsync(user);
        }

        public async void UpdateDocument(string newName,string newDescription, Template newTemplate, byte newFile)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/update_document").PutJsonAsync(document);
        }

        public async void UpdateRole(string name, int count)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/update_role").PutJsonAsync(role);
        }

        public async void CreateRole(string name,int count)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/create_role").PostJsonAsync(role);
        }

        public async void CreateDocument(string name, string description, Template template, byte file)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/create_document").PutJsonAsync(document);
        }

        public async void CreateTemplate(string name, int count)
        {
            var response = await $"{baseUrl}".AppendPathSegment("/create_template").PutJsonAsync(template);
        }
     }
}
