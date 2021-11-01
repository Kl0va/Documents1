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

namespace Documents.Xaml.Admin
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class TemplatesPage : Page
    {
        Frame rootFrame;
        public TemplatesPage()
        {
            this.InitializeComponent();
        }

        private void createTemplateBtn_Click(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate(typeof(CreateTemplatePage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            rootFrame = e.Parameter as Frame;
            Task<List<Template>> getTemplates = ApiWork.GetAllTemplates();
            getTemplates.Start();
            foreach(Template template in getTemplates.Result)
            {
                TemplatesGrid.Items.Add(template);
            }
        }
    }
}
