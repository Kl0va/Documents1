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

namespace Documents.Xaml.User
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class UserPage : Page
    {
        Frame rootFrame;
        public UserPage()
        {
          this.InitializeComponent();
        }

        private void addDocument_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateDocument));
        }



        private void documentsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(DetailsDocument));

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            rootFrame = e.Parameter as Frame;
            //Task<List<Document>> getDocuments = ApiWork.GetAllDocuments(1);
            //getDocuments.Start();
            //foreach (Document document in getDocuments.Result)
            //{
            //    documentsGrid.Items.Add(document);
            //}
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mydocuments.IsSelected)
            {
                /////
            }
            else if (alldocuments.IsSelected)
            {
                //////////
            }
            else if (waiting.IsSelected)
            {
                ////////////

            }
            else if (needforsee.IsSelected)
            {
                /////////////////
            }
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {

            mySplitView.IsPaneOpen = !mySplitView.IsPaneOpen;
            
        }
    }
}
