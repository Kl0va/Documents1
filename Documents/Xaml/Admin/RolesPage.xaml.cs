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

namespace Documents.Xaml.Admin
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class RolesPage : Page
    {
        Frame rootFrame;
        public RolesPage()
        {
            this.InitializeComponent();
            //RoleGrid.Items.Add(new Role("test", 0));
        }

        private void createRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate(typeof(CreateRolePage));
        }
        private static Role roleAdd;
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            rootFrame = e.Parameter as Frame;
            Task<List<Role>> getRoles = ApiWork.GetAllRoles();
            await getRoles.ContinueWith(t =>
            {
                foreach (Role role in getRoles.Result)
                {
                    roleAdd = role;
                }
            });
            RoleGrid.Items.Add(roleAdd);
        }
    }
}
