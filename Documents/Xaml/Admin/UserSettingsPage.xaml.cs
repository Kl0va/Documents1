using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using System.Threading.Tasks;
using Documents.Models;
using Documents.Moduls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Documents
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class UserSettingsPage : Page
    {
        private readonly List<Role> roles = new List<Role>();
        public UserSettingsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            User user = e.Parameter as User;
            headerEmail.Text = user.Email;
            Task<List<Role>> rolesTask = ApiWork.GetAllRoles();
            rolesTask.ContinueWith(task =>
            {
                roles.Clear();
                foreach(Role role in task.Result)
                {
                    roles.Add(role);
                }
            }
            );
            RoleName.ItemsSource = roles;
        }

        private static string ID;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task<List<Role>> roleTask = ApiWork.GetAllRoles();
            roleTask.ContinueWith(task =>
            {
                foreach (Role role in task.Result)
                {
                    ID = role.name;
                }
            });
            Rule ruleAdd = new Rule(CheckAdd.IsChecked.Value, CheckShow.IsChecked.Value, CheckSend.IsChecked.Value, CheckReconcile.IsChecked.Value, CheckEdit.IsChecked.Value, CheckFamiliarize.IsChecked.Value);
            ApiWork.UpdateRule(ruleAdd);
            ApiWork.UpdateUser(new User(ID));
        }
    }
}
