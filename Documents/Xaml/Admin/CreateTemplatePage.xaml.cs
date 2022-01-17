using Documents.Models;
using Documents.Moduls;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
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
                Models.Template template = e.Parameter as Models.Template;
                DocumentText.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, template.sampleByte);
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            string text = "";

            using (PdfDocument document = new PdfDocument())
            {
                //Create a new PDF document.
                PdfDocument doc = new PdfDocument();
                //Add a page to the document.
                PdfPage page = doc.Pages.Add();
                //Create PDF graphics for the page
                PdfGraphics graphics = page.Graphics;
                //Load the image as stream
                Stream imageStream = typeof(MainPage).GetTypeInfo().Assembly.GetManifestResourceStream("Documents.Assets.Header.png");
                //Load the image from the disk.
                PdfBitmap image = new PdfBitmap(imageStream);
                //Draw the image
                graphics.DrawImage(image, 0, 0);
                //Create memory stream
                MemoryStream ms = new MemoryStream();
                //Open the document in browser after saving it
                doc.Save(ms);
                //Close the document
                doc.Close(true);
                Save(ms, "Sample.pdf");
            }



            DocumentText.Document.GetText(Windows.UI.Text.TextGetOptions.None, out text);
        }


        #region Helper Methods
        public async void Save(Stream stream, string filename)
        {
            stream.Position = 0;

            StorageFile stFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.DefaultFileExtension = ".pdf";
                savePicker.SuggestedFileName = "Sample";
                savePicker.FileTypeChoices.Add("Adobe PDF Document", new List<string>() { ".pdf" });
                stFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stFile = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            }
            if (stFile != null)
            {
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.SetLength(0);
                st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                st.Flush();
                st.Dispose();
                fileStream.Dispose();
                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File created.");
                UICommand yesCmd = new UICommand("Yes");
                msgDialog.Commands.Add(yesCmd);
                UICommand noCmd = new UICommand("No");
                msgDialog.Commands.Add(noCmd);
                IUICommand cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the retrieved file 
                    bool success = await Windows.System.Launcher.LaunchFileAsync(stFile);
                }
            }
        }
        #endregion






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
