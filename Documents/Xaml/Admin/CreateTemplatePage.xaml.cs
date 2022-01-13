using Documents.Moduls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
    }
}
