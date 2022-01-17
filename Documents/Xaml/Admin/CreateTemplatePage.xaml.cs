using Documents.Models;
using Documents.Moduls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Documents.Xaml.Admin
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class CreateTemplatePage : Page
    {
        public CreateTemplatePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                Template template = e.Parameter as Template;
                DocumentText.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, template.sampleByte);
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            string text = "";
            DocumentText.Document.GetText(Windows.UI.Text.TextGetOptions.None, out text);
            Moduls.Documents.CreatePDF(text,pageHeader.Text);
        }

        private void addhat_Click(object sender, RoutedEventArgs e)
        {
            ParagraphAlignment paragraphAlign = ParagraphAlignment.Undefined;
            paragraphAlign = ParagraphAlignment.Center;
            string text = "";
            Header.Document.GetText(Windows.UI.Text.TextGetOptions.FormatRtf, out text);
            DocumentText.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, text);
        }

        private async void DocumentText_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.V && e.Key == Windows.System.VirtualKey.Control)
            {
                var dataPackageView = Windows.ApplicationModel.DataTransfer.Clipboard.GetContent();
                if (dataPackageView.Contains(StandardDataFormats.Bitmap))
                {
                    IRandomAccessStreamReference imageReceived = null;
                    try
                    {
                        imageReceived = await dataPackageView.GetBitmapAsync();
                    }
                    catch (Exception ex)
                    { }
                    if (imageReceived != null)
                    {
                        using (var imageStream = await imageReceived.OpenReadAsync())
                        {
                            var bitmapImage = new BitmapImage();
                            bitmapImage.SetSource(imageStream);
                            DocumentText.Document.Selection.InsertImage((int)bitmapImage.PixelWidth, (int)bitmapImage.PixelHeight, 0, Windows.UI.Text.VerticalCharacterAlignment.Baseline, "Image", imageStream);
                            e.Handled = true;
                            Clipboard.Clear();
                        }
                    }
                }
            }
        }

        
    }
}
