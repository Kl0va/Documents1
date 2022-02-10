using Documents.Models;
using Documents.Moduls;
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

namespace Documents.Xaml.UserPages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class FamiliarizeDoc : Page
    {
        private static DocumentForFamiliarize document;
        public FamiliarizeDoc()
        {
            this.InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            document.status = true;
            ApiWork.SaveDocFam(document);
            Frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter != null)
            {
                document = e.Parameter as Models.DocumentForFamiliarize;
                pageHeader.Text = document.documentId.name;
                DocumentText.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, document.documentId.documentsByte);
                if(document.status == true)
                {
                    Confirm.IsEnabled = false;
                }
            }
        }
    }
}
