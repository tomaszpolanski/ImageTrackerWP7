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
using Microsoft.Phone.Tasks;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using ImageLibrary;
using ModelLibrary;
using System.Xml.Serialization;
using System.Windows.Data;
using System.Globalization;
using MetroInMotionUtils;

namespace ImageTracker
{
    public partial class EditSessionPage : PhoneApplicationPage
    {

        const string PhotoName = "Photo{0}.jpg";
        public EditSessionPage()
        {
            InitializeComponent();
            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            PhotoSession dataContextSession = (App.Current as App).PhotoSessions.CreateSession();
            if (NavigationContext.QueryString.ContainsKey("id"))
            {
                string idString = NavigationContext.QueryString["id"].ToString();
                
                if (!String.IsNullOrEmpty(idString))
                {
                    dataContextSession.CopyId( (App.Current as App).PhotoSessions.GetSession(Convert.ToUInt32(idString)));
                }
            }

            MainLayout.DataContext = dataContextSession;

            base.OnNavigatedTo(e);
        }
		

        private void PhotoField_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhotoSession session = (MainLayout.DataContext as PhotoSession);
           NavigationService.Navigate( new Uri(String.Format("/SingleImagePage.xaml?image={0}"
            , session.PhotoFileName), UriKind.Relative));
        }
		
		private void TakePhoto()
		{
			CameraCaptureTask cct = new CameraCaptureTask(); // initalize task
            cct.Completed += new System.EventHandler<PhotoResult>(PhotoChooserCompleted); // wire up the event

            cct.Show(); // launch camera
		}

        private void PhotoChooserCompleted(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK) // if capture completed
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.SetSource( ImageLibrary.ImageTransformation.RotateStream( e.ChosenPhoto, 90) );
                PhotoSession session = (MainLayout.DataContext as PhotoSession);
                PhotoField.Source = bitmap;

                string photoFileName = String.Format(PhotoName, session.Id);
                
                ImageStorage.SavePhoto(photoFileName, bitmap);
                session.PhotoFileName = photoFileName;

                
            }

            
           
        }



        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        	TakePhoto();
        }

        private void ApplicationBar_Accept(object sender, EventArgs e)
        {
            PhotoSession photoSession = (MainLayout.DataContext as PhotoSession);
            photoSession.PhotoFileName = String.Format(PhotoName, photoSession.Id);
            PhotoSession existingSession = (App.Current as App).PhotoSessions.GetSession(photoSession.Id);
            if (existingSession != null)
            {
                existingSession.Copy(photoSession);
            }
            else
            {
                (App.Current as App).PhotoSessions.Add(photoSession);
            }
            
            NavigationService.GoBack();
        }

        private void ApplicationBar_Cancel(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ShowApplicationBar(object sender, RoutedEventArgs e)
        {
            ApplicationBar.IsVisible = true;
			
			
        }

        private void HideApplicationBar(object sender, RoutedEventArgs e)
        {
            ApplicationBar.IsVisible = false;
            TextBox box = (sender as TextBox);
            if (box != null)
            {
                box.SelectAll();
            }
        }

    }


}
