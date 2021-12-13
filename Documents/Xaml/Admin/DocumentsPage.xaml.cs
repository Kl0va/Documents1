﻿using Documents.Models;
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
    public sealed partial class DocumentsPage : Page
    {
        private static readonly List<Document> documents = new List<Document>();
        Frame rootFrame;
        public DocumentsPage()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            rootFrame = e.Parameter as Frame;
            Task<List<Document>> getDocuments = ApiWork.GetAllDocuments(1);
            await getDocuments.ContinueWith(t =>
            {
                documents.Clear();
                foreach (Document document in getDocuments.Result)
                {
                    documents.Add(document);
                }
            });
            documentsGrid.ItemsSource = documents;
        }
    }
}
