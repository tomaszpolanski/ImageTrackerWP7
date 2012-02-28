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
using ModelLibrary;
using System.Windows.Data;
using System.Globalization;
using MetroInMotionUtils;

namespace ImageTracker
{
    public partial class PanoramaPage : PhoneApplicationPage
    {

        private ItemFlyInAndOutAnimations _flyOutAnimation = new ItemFlyInAndOutAnimations();
        public PanoramaPage()
        {
            
            InitializeComponent();
            LayoutRoot.DataContext = (App.Current as App).PhotoSessions;
            this.Loaded += MainPage_Loaded;

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

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {       


            // grab the Title element
            var exitAnimationElement = (sender as FrameworkElement);

            if (exitAnimationElement != null)
            {
                // animate the element, navigating to the new page when the animation finishes
                _flyOutAnimation.ItemFlyOut(exitAnimationElement, () =>
                {

                    if (exitAnimationElement != null)
                    {
                        string imageIndex = exitAnimationElement.Name.Remove(0, 5); // Removing "image"
                        NavigationService.Navigate(new Uri(String.Format("/ImagePage.xaml?imageIndex={0}",
                            imageIndex), UriKind.Relative));
                    }
                });
            }
        }

        private void MainPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
        	Dispatcher.BeginInvoke(() => _flyOutAnimation.ItemFlyIn() );
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

    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return date.ToLongDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IndexToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = (int)value;
            if (index < 0 || index >= (App.Current as App).PhotoSessions.PhotoSessionCollection.Count)
            {
                return "";
            }
            return (App.Current as App).PhotoSessions.PhotoSessionCollection[index].Date.ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}