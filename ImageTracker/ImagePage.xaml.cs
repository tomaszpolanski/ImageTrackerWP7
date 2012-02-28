using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows.Media.Imaging;
using ImageLibrary;
using System.Windows.Data;
using System.Globalization;

namespace ImageTracker
{
    public partial class ImagePage : PhoneApplicationPage
    {
        public ImagePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            LayoutRoot.DataContext = (App.Current as App).PhotoSessions;
            if (NavigationContext.QueryString.ContainsKey("imageIndex"))
            {

                string imagePath = NavigationContext.QueryString["imageIndex"];
                

                if (!String.IsNullOrEmpty(imagePath))
                {
                    listBox.UpdateLayout();
                    listBox.ScrollIntoView(listBox.Items[Convert.ToInt32(imagePath)]);
                }
            }
            base.OnNavigatedTo(e);

        }

        

        
    }
}
