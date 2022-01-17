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

        private async void save_Click(object sender, RoutedEventArgs e)
        {
            //Creating a new document
            WordDocument document = new WordDocument();
            //Adding a new section to the document
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Margins.All = 72;
            //Set page size of the section
            section.PageSetup.PageSize = new SizeF(612, 792);

            //Create Paragraph styles
            WParagraphStyle style = document.AddParagraphStyle("Normal") as WParagraphStyle;
            style.CharacterFormat.FontName = "Calibri";
            style.CharacterFormat.FontSize = 11f;
            style.ParagraphFormat.BeforeSpacing = 0;
            style.ParagraphFormat.AfterSpacing = 8;
            style.ParagraphFormat.LineSpacing = 13.8f;

            style = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
            style.ApplyBaseStyle("Normal");
            style.CharacterFormat.FontName = "Calibri Light";
            style.CharacterFormat.FontSize = 16f;
            style.CharacterFormat.TextColor = Color.FromArgb(46, 116, 181);
            style.ParagraphFormat.BeforeSpacing = 12;
            style.ParagraphFormat.AfterSpacing = 0;
            style.ParagraphFormat.Keep = true;
            style.ParagraphFormat.KeepFollow = true;
            style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();
            paragraph.ApplyStyle("Normal");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            paragraph = section.AddParagraph();
            //Gets the image stream
            Stream imageStream = this.GetType().Assembly.GetManifestResourceStream("Documents.Assets.Header.png");
            IWPicture picture = paragraph.AppendPicture(imageStream);
            picture.TextWrappingStyle = TextWrappingStyle.InFrontOfText;
            picture.VerticalOrigin = VerticalOrigin.Margin;
            picture.VerticalPosition = -45;
            picture.HorizontalOrigin = HorizontalOrigin.Column;
            picture.HorizontalPosition = 0f;
            picture.WidthScale = 30;
            picture.HeightScale = 27;
            paragraph = section.AddParagraph();


            MemoryStream stream = new MemoryStream();

            //Saves the Word document to MemoryStream
            await document.SaveAsync(stream, FormatType.Docx);

            //Saves the stream as Word document file in local machine
            Save(stream, "Sample.docx");



            //string text = "";
            //DocumentText.Document.GetText(Windows.UI.Text.TextGetOptions.None, out text);
        }


        async void Save(MemoryStream streams, string filename)
        {
            streams.Position = 0;
            StorageFile stFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.DefaultFileExtension = ".docx";
                savePicker.SuggestedFileName = filename;
                savePicker.FileTypeChoices.Add("Word Documents", new List<string>() { ".docx" });
                stFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stFile = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            }
            if (stFile != null)
            {
                using (IRandomAccessStream zipStream = await stFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    //Write compressed data from memory to file
                    using (Stream outstream = zipStream.AsStreamForWrite())
                    {
                        byte[] buffer = streams.ToArray();
                        outstream.Write(buffer, 0, buffer.Length);
                        outstream.Flush();
                    }
                }
            }
            //Launch the saved Word file
            await Windows.System.Launcher.LaunchFileAsync(stFile);
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