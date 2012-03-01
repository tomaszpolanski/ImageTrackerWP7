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

namespace ImageTracker
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            LayoutRoot.DataContext = (App.Current as App).Settings;
        }

        private void HeightField_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
    }
}
