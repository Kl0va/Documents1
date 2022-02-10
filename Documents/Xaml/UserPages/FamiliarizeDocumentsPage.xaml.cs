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

namespace Documents.Xaml.UserPages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class FamiliarizeDocumentsPage : Page
    {
        public static string email;
        private static List<DocumentForFamiliarize> documentFam = new List<DocumentForFamiliarize>();
        public FamiliarizeDocumentsPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                email = e.Parameter as string;
                Task<List<DocumentForFamiliarize>> getFam = ApiWork.GetAllDocumentsForFamiliarize();
                await getFam.ContinueWith(t =>
                {
                    documentFam.Clear();
                    foreach (DocumentForFamiliarize doc in getFam.Result)
                    {
                        if (doc.employeeId == email)
                        {
                            documentFam.Add(doc);
                        }
                    }
                });

                var sortedDoc = from u in documentFam
                                orderby u.status
                                select u;

                documentsGrid.ItemsSource = sortedDoc;
                progress.Visibility = Visibility.Collapsed;
            }
        }

        private void documentsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(FamiliarizeDoc), (Models.DocumentForFamiliarize)documentsGrid.SelectedItem);
        }
    }
}
