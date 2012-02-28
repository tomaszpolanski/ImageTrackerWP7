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
            string imagePath = NavigationContext.QueryString["image"];
            if (!String.IsNullOrEmpty(imagePath))
            {
                PhotoImage.Source = ImageStorage.CreateBitmapImage(imagePath);
                WriteableBitmap something = null;
                
            }
            base.OnNavigatedTo(e);

        }

       

        
    }
}
