using Documents.Xaml.Admin;
using Documents.Xaml.User;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Documents.Moduls;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Isam.Esent;

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Task<List<User>> user = ApiWork.GetAllUsers();
            //user.Start();
            //List<User> user1 = user.Result;
            //foreach (User user2 in user1)
            //{
            //    if (Email.Text == user2.Email)
            //    {

            //        Frame.Navigate(typeof(UserPage));
            //    }
            //}
            Frame.Navigate(typeof(UserPage));
        }
    }
}
