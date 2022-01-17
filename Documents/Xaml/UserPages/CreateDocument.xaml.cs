using Documents.Models;
using Documents.Moduls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Documents.Xaml.UserPage
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class CreateDocument : Page
    {
       
        public CreateDocument()
        {
            //Template t = new Template("Шаблоныч", 1);
            this.InitializeComponent();
            //documentsGrid.Items.Add(new Document("Документыч1.docxыч", "На документычах", t));
            Load();
        }
        private static readonly List<User> users = new List<User>();
        private static readonly List<Template> templates = new List<Template>();
        private void redact_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RedactDocument));
        }
        public async void Load()
        {
            progress.Visibility = Visibility.Visible;
            Task<List<User>> getUsers = ApiWork.GetAllUsers();
            Task<List<Template>> getTamplates = ApiWork.GetAllTemplates();
            await getTamplates.ContinueWith(t =>
            {
                templates.Clear();
                foreach(Template template in getTamplates.Result)
                {
                    templates.Add(template);
                }
            });
            foreach (Template template in templates) {
                docTypeCombo.Items.Add(template);
            }

            await getUsers.ContinueWith(t =>
            {
                users.Clear();
                foreach (User user in getUsers.Result)
                {
                    users.Add(user);
                }
            });
            foreach (User user1 in users)
            {
                forReconciliationCombo.Items.Add(user1.Email);
            }
            //(docTypeCombo.SelectedItem as Template).Name;
            //ApiWork.AddDocument(docName.Text,docDescription.Text,docTypeCombo.SelectedItem.ToString(),(forReconciliationCombo.SelectedItem as DocumentForReconcile).ID,);
            progress.Visibility = Visibility.Collapsed;
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
