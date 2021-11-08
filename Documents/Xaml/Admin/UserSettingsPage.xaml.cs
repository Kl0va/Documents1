﻿using System;
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
        private static readonly List<Role> roleAdd;
        public UserSettingsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            User user = e.Parameter as User;
            headerEmail.Text = user.Email;
            Task<List<Role>> roles = ApiWork.GetAllRoles();
            roles.ContinueWith(t =>
            {
                roleAdd.Clear();
                foreach(Role role in roles.Result)
                {
                    roleAdd.Add(role);
                }
            }
            );
            foreach(Role roleCheck in roleAdd)
            {
                RoleName.Items.Add(roleCheck.Name);
            }
        }
    }
}
