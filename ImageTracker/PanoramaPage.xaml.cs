﻿using System;
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
using ModelLibrary;
using System.Windows.Data;
using System.Globalization;

namespace ImageTracker
{
    public partial class PanoramaPage : PhoneApplicationPage
    {
        public PanoramaPage()
        {
            
            InitializeComponent();
            LayoutRoot.DataContext = (App.Current as App).PhotoSessions;

        }

        private void Item_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri(String.Format("/EditSessionPage.xaml?id={0}"
                , ((sender as FrameworkElement).DataContext as PhotoSession).Id), UriKind.Relative));
        }


        private void AddNew_Clicked(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/EditSessionPage.xaml", UriKind.Relative));
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            PhotoSession item = element.DataContext as PhotoSession;
            (App.Current as App).PhotoSessions.Remove(item);
        }

    }
    public class WeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double weight = (double)value;
            return String.Format("{0} kg", weight);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}