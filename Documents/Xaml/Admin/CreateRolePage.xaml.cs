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
        public CreateRolePage()
        {
            this.InitializeComponent();
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
            if (e.Parameter.GetType() == typeof(Role))
            {
                Role role = e.Parameter as Role;
                roleName.Text = role.Name;
                createRole.IsEnabled = false;
                Task<List<Template>> templatesTask = ApiWork.GetAllTemplates();
                templatesTask.ContinueWith(task =>
                {
                    
                });
            }
            else
            {
                UIEnabled = false;
            }

        }

        private async void createRole_Click(object sender, RoutedEventArgs e)
        {
            Task<List<Role>> getRoles = ApiWork.GetAllRoles();
            await getRoles.ContinueWith(task =>
            {
                string name = roleName.Text;
                if (task.Result.Any(r => r.Name == name))
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
