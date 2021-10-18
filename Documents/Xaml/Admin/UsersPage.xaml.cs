using Documents.Xaml.Admin;
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
        Frame rootFrame;
        public UsersPage()
        {
            this.InitializeComponent();
            UserGrid.Items.Add(new User("test@gmail.com", "test", "Test Testovich Test"));
        }

        private void UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rootFrame.Navigate(typeof(UserSettingsPage), e.AddedItems[0] as User);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            rootFrame = e.Parameter as Frame;
        }
    }
}
