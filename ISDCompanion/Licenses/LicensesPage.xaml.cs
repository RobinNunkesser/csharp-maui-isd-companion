using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class LicensesPage : ContentPage
    {
        public LicensesPage(string licenseFile)
        {
            InitializeComponent();

            var assembly = typeof(LicensesPage).GetTypeInfo().Assembly;
            Stream stream = assembly
                .GetManifestResourceStream($"ISDCompanion.Licenses.{licenseFile}");
            string html = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            var htmlSource = new HtmlWebViewSource
            {
                Html = html
            };
            Browser.Source = htmlSource;
        }
    }
}
