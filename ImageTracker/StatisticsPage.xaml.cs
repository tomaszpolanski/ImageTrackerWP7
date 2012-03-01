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
using ImageLibrary;
using ModelLibrary;

namespace ImageTracker
{
    public partial class StatisticsPage : PhoneApplicationPage
    {
        public StatisticsPage()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            SpaceUsed.Text = CalculateSpaceUsed();
            Sessions.Text = CalculateSessions();
            AvaWeight.Text = CalculateAvarageWeight();
            CurrentWeightLoss.Text = CalculateCurrentWeightLoss();
            CurrentBMI.Text = CalculateCurrentBMI();
        }


        private string CalculateSpaceUsed()
        {
            long spaceUsed = ImageStorage.SpaceUsed(); ;
            const double mb = 1048576;
            return String.Format("{0:0.00} mb", (double)((double)spaceUsed / mb));
        }

        private string CalculateSessions()
        {
            return String.Format("{0}", (App.Current as App).PhotoSessions.PhotoSessionCollection.Count);
        }

        private string CalculateAvarageWeight()
        {
            return String.Format("{0:0.0} kg", PhotoSessionStatistics.AvarageWeight((App.Current as App).PhotoSessions));
        }

        private string CalculateCurrentWeightLoss()
        {
            return String.Format("{0:0.0} kg", PhotoSessionStatistics.CurrentWeightLoss((App.Current as App).PhotoSessions));
        }

        private string CalculateCurrentBMI()
        {
            App app = (App.Current as App);
            return String.Format("{0:0.00}", PhotoSessionStatistics.CurrentBMI(app.PhotoSessions, app.Settings.Height));
        }
    }
}
