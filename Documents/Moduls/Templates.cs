using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Reflection;
using System.IO;


namespace Documents.Moduls
{
    static class Templates
    {
        public static  void Create(WordDocument document, WSection section)
        {
            //Создание новго документа 
            document = new WordDocument();
            //Добавление нового раздела
            section = document.AddSection() as WSection;
            //Размер страниы 
            section.PageSetup.PageSize = new SizeF(612, 792);
        }

        public static void Paragraph(WordDocument document, WSection section)
        {
            //Параграф
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
        }

        public static void Table(WSection section)
        {
            IWTable table = section.AddTable();
            table.ResetCells(3, 2);
            table.TableFormat.Borders.BorderType = BorderStyle.None;
            table.TableFormat.IsAutoResized = true;
        }

        public static async void SaveDoc(WordDocument document)
        {
            MemoryStream stream = new MemoryStream();
            await document.SaveAsync(stream, FormatType.Docx);

            Documents.Save(stream, "Sample.docx");
        }
    }
}
