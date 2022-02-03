using Documents.Models;
using Documents.Moduls;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Documents.Xaml.UserPage
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class CreateDocument : Page
    {
        private static string Header;
        private static readonly List<User> usersFam = new List<User>();
        private static readonly List<Models.Documents> documents = new List<Models.Documents>();
        private static string selectedValue;
        public CreateDocument()
        {
            this.InitializeComponent();
            Load();
        }
        private static readonly List<User> users = new List<User>();
        private static readonly List<Models.Template> templates = new List<Models.Template>();
        private static readonly List<Models.Documents> documents1 = new List<Models.Documents>();
        private void redact_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RedactDocument));
        }
        /// <summary>
        /// Подгрузка всего нужного
        /// </summary>
        public async void Load()
        {
            TimeOfAgreement.IsEnabled = false;
            addsigner.IsEnabled = false;
            Signatory.IsEnabled = false;
            familiarize.IsEnabled = false;
            add_fam.IsEnabled = false;
            Task<List<User>> userTask = ApiWork.GetAllUsers();
            await userTask.ContinueWith(task =>
            {
                users.Clear();
                foreach (User user in userTask.Result)
                {
                    users.Add(user);
                }
            });
            Task<List<Models.Template>> getTemplates = ApiWork.GetAllTemplates();
            await getTemplates.ContinueWith(t =>
            {
                templates.Clear();
                foreach (Models.Template template in getTemplates.Result)
                {
                    templates.Add(template);
                }
            });
            foreach(Models.Template template1 in templates)
            {
                Template.Items.Add(template1.name);
            }
            List<string> FIO = new List<string>();
            foreach (User user1 in users)
            {
                FIO.Add(user1.FirstName + " " + user1.SecondName + " " + user1.MiddleName);
            }
            foreach (string fio in FIO)
            {
                Signatory.Items.Add(fio);
                familiarize.Items.Add(fio);
            }
        }
        private static List<string> signerList = new List<string>();
        private static bool first = true;
        /// <summary>
        /// Добавление подписанта в текст и лист
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addsigner_Click(object sender, RoutedEventArgs e)
        {
            string text = "";
            DocumentText.Document.GetText(Windows.UI.Text.TextGetOptions.None, out text);
            if (first)
            {
                text += "\n\nС приказом ознакомлен(а):\n" + Signatory.SelectedValue.ToString();
                DocumentText.Document.SetText(TextSetOptions.FormatRtf, text);
                signerList.Add(Signatory.SelectedValue.ToString());
                first = false;
            }
            else
            {
                text += "\n" + Signatory.SelectedValue.ToString();
                DocumentText.Document.SetText(TextSetOptions.FormatRtf, text);
                signerList.Add(Signatory.SelectedValue.ToString());
            }
        }
        public static WordDocument document = new WordDocument();
        public static WSection section = document.AddSection() as WSection;
        /// <summary>
        /// Сохранение дока
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void save_Click(object sender, RoutedEventArgs e)
        {
            Task<List<Models.Documents>> getTemplates = ApiWork.GetAllDocuments();
            await getTemplates.ContinueWith(t =>
            {
                documents1.Clear();
                foreach (Models.Documents template1 in getTemplates.Result)
                {
                    documents1.Add(template1);
                }
            });
            bool check = true;
            foreach (Models.Documents checkName in documents1)
            {
                if (checkName.name == pageHeader.Text)
                {
                    ContentDialog errorDialog = new ContentDialog()
                    {
                        Title = "Ошибка",
                        Content = "Документ с таким именем уже существует",
                        PrimaryButtonText = "Ок"
                    };
                    ContentDialogResult result = await errorDialog.ShowAsync();
                    check = false;
                    break;
                }
            }
            if (check)
            {
                //Отступы
                section.PageSetup.Margins.All = 72;
                //размер окна документа
                section.PageSetup.PageSize = new SizeF(612, 792);

                //Создание стиля
                WParagraphStyle style = document.AddParagraphStyle("Normal") as WParagraphStyle;
                style.CharacterFormat.FontName = "Times New Roman";
                style.CharacterFormat.FontSize = 12f;
                style.ParagraphFormat.BeforeSpacing = 0;
                style.ParagraphFormat.LineSpacing = 13.8f;
                //Создание стиля 
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
                //Создание параграфа
                IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();
                paragraph.ApplyStyle("Normal");
                paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                paragraph = section.AddParagraph();
                //Добавление картинки
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
                string text = "";
                //Добавление текста
                DocumentText.Document.GetText(Windows.UI.Text.TextGetOptions.None, out text);
                string typeName = pageHeader.Text;
                DateTime nowDate = DateTime.Now;
                string inputText = "от " + nowDate.ToShortDateString() + "\n" + typeName + "\n" + text;
                DocumentText.Document.SetText(TextSetOptions.FormatRtf, inputText);
                WTextRange textRange = new WTextRange(document);
                textRange = paragraph.AppendText("\n\n" + inputText) as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;

                MemoryStream stream = new MemoryStream();

                //Saves the Word document to MemoryStream
                await document.SaveAsync(stream, FormatType.Docx);

                //Saves the stream as Word document file in local machine
                Save(stream, "Sample.docx");
                //Models.Documents documentSave = new Models.Documents(pageHeader.Text,Template.SelectedValue.ToString(),"Учитель",inputText);
                ApiWork.AddDocument(pageHeader.Text, Template.SelectedValue.ToString(), "Учитель", inputText);
                Thread.Sleep(500);
                //Сохранение подписантов
                if(signerList.Count > 0)
                {
                    Header = pageHeader.Text;
                    Task<List<Models.Documents>> getDocuments = ApiWork.GetAllDocuments();
                    await getDocuments.ContinueWith(t =>
                    {
                        documents.Clear();
                        foreach (Models.Documents document in getDocuments.Result)
                        {
                            if (document.name == Header)
                            {
                                documents.Add(document);
                            }
                        }
                    });
                    Task<List<User>> userTask = ApiWork.GetAllUsers();
                    await userTask.ContinueWith(task =>
                    {
                        usersFam.Clear();
                        foreach (User user in userTask.Result)
                        {
                            foreach (string sigFio in signerList)
                            {
                                string FIO = user.FirstName + " " + user.SecondName + " " + user.MiddleName;
                                if (FIO == sigFio)
                                {
                                    usersFam.Add(user);
                                }
                            }
                        }
                    });
                    string emp_id = "";
                    int doc_id = 0;
                    foreach (User user1 in usersFam)
                    {
                        emp_id = user1.id;
                    }
                    foreach (Models.Documents documents1 in documents)
                    {
                        doc_id = documents1.id;
                    }
                    Models.DocumentForReconcile documentForReconcile = new DocumentForReconcile(emp_id,TimeOfAgreement.Date.DateTime,false,doc_id);
                    ApiWork.AddDocumentForReconcile(documentForReconcile);
                }
                familiarize.IsEnabled = true;
                add_fam.IsEnabled = true;
            }
        }
        private static List<Models.Template> searchTemplate = new List<Models.Template>();
        private static string header; 
        /// <summary>
        /// Подгрузка шаблона в текст
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Template_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TimeOfAgreement.IsEnabled = true;
            addsigner.IsEnabled = true;
            Signatory.IsEnabled = true;
            header = Template.SelectedValue.ToString();
            Task<List<Models.Template>> getTemplates = ApiWork.GetAllTemplates();
            await getTemplates.ContinueWith(t =>
            {
                searchTemplate.Clear();
                foreach (Models.Template template in getTemplates.Result)
                {
                    if (template.name == header)
                    {
                        searchTemplate.Add(template);
                    }
                }
            });
            foreach (Models.Template template1 in searchTemplate) {
                DocumentText.Document.SetText(TextSetOptions.FormatRtf,template1.sampleByte);
            }
        }
        /// <summary>
        /// Метод сохранения
        /// </summary>
        /// <param name="streams"></param>
        /// <param name="filename"></param>
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
                    using (Stream outstream = zipStream.AsStreamForWrite())
                    {
                        byte[] buffer = streams.ToArray();
                        outstream.Write(buffer, 0, buffer.Length);
                        outstream.Flush();
                    }
                }
            }
            await Windows.System.Launcher.LaunchFileAsync(stFile);
        }
        /// <summary>
        /// Сохранение человека на рассмотрение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void add_fam_Click(object sender, RoutedEventArgs e)
        {
            Header = pageHeader.Text;
            Task<List<Models.Documents>> getDocuments = ApiWork.GetAllDocuments();
            await getDocuments.ContinueWith(t =>
            {
                documents.Clear();
                foreach (Models.Documents document in getDocuments.Result)
                {
                    if (document.name == Header)
                    {
                        documents.Add(document);
                    }
                }
            });
            selectedValue = familiarize.SelectedValue.ToString();
            Task<List<User>> userTask = ApiWork.GetAllUsers();
            await userTask.ContinueWith(task =>
            {
                usersFam.Clear();
                foreach (User user in userTask.Result)
                {
                    string FIO = user.FirstName + " " + user.SecondName + " " + user.MiddleName;
                    if(FIO == selectedValue)
                    {
                        usersFam.Add(user);
                    }
                }
            });
            string emp_id = "";
            int doc_id = 0;
            foreach (User user1 in usersFam)
            {
                emp_id = user1.id;
            }
            foreach(Models.Documents documents1 in documents)
            {
                doc_id = documents1.id;
            }
            Models.DocumentForFamiliarize documentForFamiliarize = new DocumentForFamiliarize(emp_id,doc_id);
            ApiWork.AddFamiliarize(documentForFamiliarize);
        }
    }
}
