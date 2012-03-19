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
            CurrentWeight.Text = CalculateCurrentWeight();
            CurrentWeightLoss.Text = CalculateCurrentWeightLoss();
            CurrentBMI.Text = CalculateCurrentBMI();
            HeighestWeight.Text = CalculateHighestWeight();
            LowestWeight.Text = CalculateLowestWeight();
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

        private string CalculateCurrentWeight()
        {
            return String.Format("{0:0.0} kg", PhotoSessionStatistics.CurrentWeight((App.Current as App).PhotoSessions));
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

        private string CalculateHighestWeight()
        {
            App app = (App.Current as App);
            return String.Format("{0:0.0} kg", PhotoSessionStatistics.HighestWeight(app.PhotoSessions));
        }

        private string CalculateLowestWeight()
        {
            App app = (App.Current as App);
            return String.Format("{0:0.0} kg", PhotoSessionStatistics.LowestWeight(app.PhotoSessions));
        }
    }
}
