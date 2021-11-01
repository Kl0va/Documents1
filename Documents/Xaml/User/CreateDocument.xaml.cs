﻿using Documents.Models;
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

namespace Documents.Xaml.User
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class CreateDocument : Page
    {
        public CreateDocument()
        {
            //Template t = new Template("Шаблоныч", 1);
            this.InitializeComponent();
            //documentsGrid.Items.Add(new Document("Документыч1.docxыч", "На документычах", t));
        }

        private void redact_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RedactDocument));
        }
    }
}
