using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.IO;


namespace ImageTracker
{
    

    [XmlRoot(ElementName = "settings")]
    public class Settings : INotifyPropertyChanged
	{
        [XmlIgnore]
        const string DataFile = "Settings.xml";

        private double _height = 182;
        [XmlElement("height")]
        public double Height
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    _height = value;
                    OnPropertyChanged(this, "Height");
                }
            }
        }

		public Settings()
		{
		}

        public void Save()
        {
            using (IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                TextWriter writer = null;
                isoStorage.DeleteFile(DataFile);

                    IsolatedStorageFileStream file = isoStorage.OpenFile(DataFile, FileMode.OpenOrCreate);

                    writer = new StreamWriter(file);

                    XmlSerializer xs = new XmlSerializer(this.GetType());

                    xs.Serialize(writer, this);
                    writer.Close();
                
            }
        }

        public static Settings Load()
        {
            using (IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //isoStorage.DeleteFile(DataFile);
                TextReader reader = null;
                IsolatedStorageFileStream file = null;
                Settings settings = null;
                try
                {
                    if (isoStorage.FileExists(DataFile))
                    {
                        file = isoStorage.OpenFile(DataFile, FileMode.Open);

                        reader = new StreamReader(file);
                        XmlSerializer xs = new XmlSerializer(typeof(Settings));
                        settings = (Settings)xs.Deserialize(reader);
                    }
                }
                catch (InvalidOperationException exception)
                {
                    isoStorage.DeleteFile(DataFile);
                    MessageBox.Show(exception.Message);
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                    } if (file != null)
                    {
                        file.Close();
                    }


                }
                if (settings == null)
                {
                    settings = new Settings();
                }
                return settings;

            }
        }

        #region Property changed
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(object sender, string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
	}
}