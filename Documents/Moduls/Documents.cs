using System;
using System.Collections.Generic;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using System.IO;
using Syncfusion.DocIO.DLS;

namespace Documents.Moduls
{
   static class Documents
    {
        //Сохранение файла 
        public static async void Save(MemoryStream streams, string filename)
        {
            streams.Position = 0;
            StorageFile stFile;

            if (filename != null)
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
                //Сжатие файлы в byte и запись
                using (IRandomAccessStream zipStream = await stFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    using (Stream outstream = zipStream.AsStreamForWrite())
                    {
                        byte[] buffer = streams.ToArray();
                        outstream.Write(buffer, 0, buffer.Length);
                        outstream.Flush();
                    }
                }
            }
        }

        static  void Get(byte[] buffer, MemoryStream streams)
        {
            Stream stream = null;
            buffer = streams.ToArray();
            stream.Read(buffer, 0, buffer.Length);
            stream.Flush();
        }
      
    }
}