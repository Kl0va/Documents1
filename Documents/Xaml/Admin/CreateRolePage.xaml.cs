using Documents.Models;
using Documents.Moduls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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

namespace Documents.Xaml.Admin
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class CreateRolePage : Page
    {
        private static List<Template> templates = new List<Template>();
        public CreateRolePage()
        {
            this.InitializeComponent();
            Task<List<Template>> templatesTask = ApiWork.GetAllTemplates();
            templatesTask.ContinueWith(task =>
            {
                templates.Clear();
                foreach (Models.Template template in templatesTask.Result)
                {
                    templates.Add(template);
                }
            });
            templatesCombo.ItemsSource = templates;
        }



        private bool UIEnabled
        {
            set
            {
                templatesCombo.IsEnabled = value;
                deleteCheck.IsEnabled = value;
                checkCheck.IsEnabled = value;
                changeCheck.IsEnabled = value;
                saveRole.IsEnabled = value;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                if (e.Parameter.GetType() == typeof(Role))
                {
                    Role role = e.Parameter as Role;
                    roleName.Text = role.name;
                    createRole.IsEnabled = false;                  
                }
                else
                {
                    UIEnabled = false;
                }
            }
            catch
            {

            }

        }

        private async void createRole_Click(object sender, RoutedEventArgs e)
        {
            Task<List<Role>> getRoles = ApiWork.GetAllRoles();
            string name = roleName.Text;
            await getRoles.ContinueWith(task =>
            {
                if (task.Result.Any(r => r.name == name))
                {
                    ContentDialog alreadyExistDialog = new ContentDialog()
                    {
                        Title = "Ошибка",
                        Content = "Роль с таким названием уже существует",
                        PrimaryButtonText = "ОК"
                    };
                }
                else
                {
                    ApiWork.AddRole(new Role(name));
                }
            });
        }

        private void saveRole_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
