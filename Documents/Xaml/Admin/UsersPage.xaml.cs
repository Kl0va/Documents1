using Documents.Xaml.Admin;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Documents
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class UsersPage : Page
    {
        public UsersPage()
        {
            this.InitializeComponent();
            UserList.Items.Add(new User("test@gmail.com", "test"));
        }

        private void UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(UserSettingsPage), new User("test", "Test"));
        }
    }
}
