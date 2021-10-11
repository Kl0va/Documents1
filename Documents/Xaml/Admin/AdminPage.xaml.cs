using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
    public sealed partial class AdminPage : Page
    {
        public AdminPage()
        {
            this.InitializeComponent();
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (documents.IsSelected)
            {
                myFrame.Navigate(typeof(DocumentsPage));
                pageHeader.Text = "Документы";
            }
            else if (templates.IsSelected)
            {
                myFrame.Navigate(typeof(TemplatesPage));
                pageHeader.Text = "Шаблоны документов";
            }
            else if (users.IsSelected)
            {
                NavigateUsers();
                
            }
            else if (roles.IsSelected)
            {
                myFrame.Navigate(typeof(RolesPage));
                pageHeader.Text = "Роли пользователей";
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            mySplitView.IsPaneOpen = !mySplitView.IsPaneOpen;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigateUsers();
        }

        private void NavigateUsers()
        {
            myFrame.Navigate(typeof(UsersPage), Frame);
            users.IsSelected = true;
            pageHeader.Text = "Пользователи";
        }
    }
}
