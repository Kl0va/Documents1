using System;
using System.Collections.Generic;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using System.IO;
using Syncfusion.DocIO.DLS;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Windows.UI.Popups;

namespace Documents.Moduls
{
   static class Documents
    {
        public static void CreatePDF(string text,string name)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            graphics.DrawString(text, font, PdfBrushes.Black, new System.Drawing.PointF(0,0));
            MemoryStream ms = new MemoryStream();
            document.Save(ms);
            document.Close(true);
            Models.Template template = new Models.Template(name,text);
            ApiWork.AddTemplate(template);
            Save(ms, name);
        }

        public static async void Save(Stream stream, string filename)
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
                MessageDialog msgDialog = new MessageDialog("Файл создан");
                IUICommand cmd = await msgDialog.ShowAsync();
                //if (cmd == yesCmd)
                //{
                //    bool success = await Windows.System.Launcher.LaunchFileAsync(stFile);
                //}
            }
        }

    }
}