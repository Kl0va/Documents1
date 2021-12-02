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
            templatesCombo.IsEnabled = false;
            deleteCheck.IsEnabled = false;
            checkCheck.IsEnabled = false;
            changeCheck.IsEnabled = false;
            saveRole.IsEnabled = false;
        }

        private async void createRole_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            var role = new Role(roleName.Text);
            Task<List<Role>> getRoles = ApiWork.GetAllRoles();
            await getRoles.ContinueWith(t =>
            {
                foreach(Role roles in getRoles.Result)
                {
                    if(roleName.Text == roles.Name)
                    {
                        ContentDialog deleteFileDialog = new ContentDialog()
                        {
                            Title = "Ошибка",
                            Content = "Роль с таким названием уже существует",
                            PrimaryButtonText = "ОК"
                        };
                        check = false;
                    }
                }
            });
            Thread.Sleep(100);
            if (check) 
            { 
                ApiWork.AddRole(role);
                templatesCombo.IsEnabled = true;
                deleteCheck.IsEnabled = true;
                checkCheck.IsEnabled = true;
                changeCheck.IsEnabled = true;
                saveRole.IsEnabled = true;
                createRole.IsEnabled = false;
            }
        }

        private void saveRole_Click(object sender, RoutedEventArgs e)
        {
                
        }
    }
}
