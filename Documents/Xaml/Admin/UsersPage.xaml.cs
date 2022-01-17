using Documents.Moduls;
using Documents.Xaml.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Documents
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class UsersPage : Page
    {
        private static readonly List<User> users = new List<User>();
        Frame rootFrame;
        public UsersPage()
        {
            this.InitializeComponent();
            //UserGrid.Items.Add(new User("test@gmail.com", "test", "Test Testovich Test"));
        }
        private void UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rootFrame.Navigate(typeof(UserSettingsPage), e.AddedItems[0] as User);
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            progress.Visibility = Visibility.Visible;
            rootFrame = e.Parameter as Frame;
            Task<List<User>> getUsers = ApiWork.GetAllUsers();
            await getUsers.ContinueWith(t =>
            {
                users.Clear();
                foreach (User user in getUsers.Result)
                {
                    users.Add(user);
                }
            });
            UserGrid.ItemsSource = users;
            progress.Visibility = Visibility.Collapsed;
        }
    }
}
