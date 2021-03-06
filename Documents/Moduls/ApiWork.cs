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

        public static async Task<List<Models.Documents>> GetAllAdminDocuments()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/admin").AppendPathSegment("/document").GetStringAsync();

            List<Models.Documents> documents = JsonConvert.DeserializeObject<List<Models.Documents>>(response);

            return documents;
        }


        public static async Task<List<Models.Documents>> GetAllDocuments()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/admin").AppendPathSegment("/document").GetStringAsync();
            
            List<Models.Documents> documents = JsonConvert.DeserializeObject<List<Models.Documents>>(response);

            return documents;
        }

        public static async Task<List<Models.DocumentForFamiliarize>> GetAllDocumentsForFamiliarize()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/admin").AppendPathSegment("/documentforfamiliarize").GetStringAsync();

            List<Models.DocumentForFamiliarize> documents = JsonConvert.DeserializeObject<List<Models.DocumentForFamiliarize>>(response);

            return documents;
        }

        public static async Task<List<Models.DocumentForReconcile>> GetAllDocumentsForReconcile()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/admin").AppendPathSegment("/documentforreconcile").GetStringAsync();

            List<Models.DocumentForReconcile> documents = JsonConvert.DeserializeObject<List<Models.DocumentForReconcile>>(response);

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
        public static async Task<List<Restriction>> GetAllRestrictions()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/restriction").GetStringAsync();

            List<Restriction> documents = JsonConvert.DeserializeObject<List<Restriction>>(response);

            return documents;
        }

        public static async void UpdateUser(User user) => await $"{baseUrl}".AppendPathSegment("/user").PutJsonAsync(user).ReceiveString();
        
        public static async void UpdateRule(Rule rule) => await $"{baseUrl}".AppendPathSegment("/rule").PutJsonAsync(rule).ReceiveString();
        

        public static async void UpdateDocument(Models.Documents document) => await $"{baseUrl}".AppendPathSegment("/document").PutJsonAsync(document).ReceiveString();
        

        public static async void UpdateRole(Role role) => await $"{baseUrl}".AppendPathSegment("/role").PutJsonAsync(role).ReceiveString();

        public static async void UpdateRestriction(Restriction restriction) => await $"{baseUrl}".AppendPathSegment("/restriction").PutJsonAsync(restriction).ReceiveString();

        public static async void AddRole(Role role) => await $"{baseUrl}".AppendPathSegment("/role").PostJsonAsync(role).ReceiveString();
        


        public static void AddDocument(string name, string template,string employee_id, string file) => 
            AddDocument(new Models.Documents(name,template,employee_id,file));
        
        private static async void AddDocument(Models.Documents document) => await $"{baseUrl}".AppendPathSegment("/document").PostJsonAsync(document).ReceiveString();
        public static async void AddFamiliarize(Models.DocumentForFamiliarize documentForFamiliarize) => await $"{baseUrl}".AppendPathSegment("/documentforfamiliarize").PostJsonAsync(documentForFamiliarize).ReceiveString();


        public static async void AddTemplate(Template template) => await $"{baseUrl}".AppendPathSegment("/template").PostJsonAsync(template).ReceiveString();

        public static async void AddRestriction(Restriction restriction) => await $"{baseUrl}".AppendPathSegment("/restriction").PostJsonAsync(restriction).ReceiveString();
        public static async void AddDocumentForReconcile(DocumentForReconcile documentForReconcile) => await $"{baseUrl}".AppendPathSegment("/documentforreconcile").PostJsonAsync(documentForReconcile).ReceiveString();
        public static async void SaveTemplate(Models.Template template) => await $"{baseUrl}".AppendPathSegment("/template").PutJsonAsync(template).ReceiveString();
        public static async void SaveDocFam(Models.DocumentForFamiliarize documentForFamiliarize) => await $"{baseUrl}".AppendPathSegment("/documentforfamiliarize").PutJsonAsync(documentForFamiliarize).ReceiveString();
    }
}
