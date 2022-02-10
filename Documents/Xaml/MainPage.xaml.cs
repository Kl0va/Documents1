using Documents.Xaml.Admin;
using Documents.Xaml.UserPage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Documents.Moduls;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Isam.Esent;
using Windows.UI.Xaml.Media.Imaging;
using System;
using Windows.UI.Xaml.Navigation;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Services;
using System.IO;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace Documents
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminPage));
        }

        private static string email;
        private static string password;
        private static List<User> users = new List<User>();
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            email = Email.Text;
            password = Password.Text;
            Task<List<User>> userTask = ApiWork.GetAllUsers();
            await userTask.ContinueWith(task =>
            {
                users.Clear();
                foreach (User user in userTask.Result)
                {
                    if (email == user.Email && password == user.Password)
                    {
                        users.Add(user);
                    }
                }
            });
            foreach (User user1 in users)
            {
                Frame.Navigate(typeof(UserPage));
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var isDark = Application.Current.RequestedTheme == ApplicationTheme.Dark;
            if (isDark == true)
            {
                logo.Source = new BitmapImage(new Uri("ms-appx:///Assets/white.png"));
            }
            else
            {
                logo.Source = new BitmapImage(new Uri("ms-appx:///Assets/Wide310x150Logo.scale-200.png"));
            }
        }
    }
}
