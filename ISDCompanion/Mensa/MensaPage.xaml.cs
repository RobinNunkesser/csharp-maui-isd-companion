using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class MensaPage : ContentPage
    {
        public MensaPage()
        {
            InitializeComponent();
            Browser.Source = "https://www.studierendenwerk-pb.de/gastronomie/speiseplaene/";
        }
    }
}
