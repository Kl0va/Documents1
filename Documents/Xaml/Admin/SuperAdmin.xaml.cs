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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Documents
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class SuperAdmin : Page
    {
        public SuperAdmin()
        {
            this.InitializeComponent();
            UserList.Items.Add(new User("test@gmail.com", "test"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(UserSettings));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
