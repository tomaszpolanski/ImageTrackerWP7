using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.IO;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace ModelLibrary
{

    [XmlRoot(ElementName = "photoSession")]
    public class PhotoSession: INotifyPropertyChanged
    {
        private uint _id;
        [XmlAttribute("id")]
        public uint Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged(this, "Id");
                }
            }
        }
        private DateTime _date;
        [XmlElement("date")]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (value != _date)
                {
                    _date = value;
                    OnPropertyChanged(this, "Date");
                }
            }
        }
        private double _weight;
        [XmlElement("weight")]
        public double Weight
        {
            get { return _weight; }
            set
            {
                if (value != _weight)
                {
                    _weight = value;
                    OnPropertyChanged(this, "Weight");
                }
            }
        }
        private double _fat;
        [XmlElement("fat")]
        public double Fat
        {
            get { return _fat; }
            set
            {
                if (value != _fat)
                {
                    _fat = value;
                    OnPropertyChanged(this, "Fat");
                }
            }
        }
        private string _photoFileName;
        [XmlElement("photoFileName")]
        public string PhotoFileName
        {
            get { return _photoFileName; }
            set
            {
                if (value != _photoFileName)
                {
                    _photoFileName = value;
                    OnPropertyChanged(this, "PhotoFileName");
                }
            }
        }
        private BitmapImage _photo;
        [XmlIgnore]
        public BitmapImage Photo
        {
            get { return _photo; }
            set
            {
                if (value != _photo)
                {
                    _photo = value;
                    OnPropertyChanged(this, "Photo");
                }
            }
        }


        public PhotoSession(uint id, DateTime date)
        {
            _id = id;
            _date = date;
            this.PropertyChanged += new PropertyChangedEventHandler(PhotoSession_PropertyChanged); 
        }

        public PhotoSession()
        {
            this.PropertyChanged += new PropertyChangedEventHandler(PhotoSession_PropertyChanged); 
        }

        public void Copy(PhotoSession other)
        {
            if (other != null)
            {
                Date = other.Date;
                Weight = other.Weight;
                Fat = other.Fat;
                Photo = other.Photo;
                PhotoFileName = other.PhotoFileName;
            }
        }

        public void CopyId(PhotoSession other)
        {
            if (other != null)
            {
                Id = other.Id;
                Copy(other);
            }
        }

        void PhotoSession_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "PhotoFileName")
            {
                LoadImage();
            }
        }

        private void LoadImage()
        {
            Photo = ImageLibrary.ImageStorage.CreateBitmapImage(PhotoFileName);
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

    public class PhotoSessions
    {
        const string DataFile = "Test.xml";

        public ObservableCollection<PhotoSession> PhotoSessionCollection { get; private set; }

        public PhotoSessions()
        {
            PhotoSessionCollection = new ObservableCollection<PhotoSession>();
            PhotoSessionCollection.Add(new PhotoSession(1, DateTime.Now));
            PhotoSessionCollection.Add(new PhotoSession(2, DateTime.Now));
        }

        public void Add(PhotoSession session)
        {
            PhotoSession existingSession = GetSession(session.Id);
            if (existingSession == null)
            {
                PhotoSessionCollection.Add(session);
            }
        }

        public void Remove(PhotoSession session)
        {
            PhotoSession existingSession = GetSession(session.Id);
            if (existingSession != null)
            {
                PhotoSessionCollection.Remove(existingSession);
            }
        }
        public int Find(uint id)
        {
            for (int i = 0; i < PhotoSessionCollection.Count; ++i)
            {
                if (PhotoSessionCollection[i].Id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Save()
        {
            using (IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                TextWriter writer = null;
                isoStorage.DeleteFile(DataFile);
                if (PhotoSessionCollection.Count > 0)
                {
                    IsolatedStorageFileStream file = isoStorage.OpenFile(DataFile, FileMode.OpenOrCreate);

                    writer = new StreamWriter(file);

                    XmlSerializer xs = new XmlSerializer(PhotoSessionCollection.GetType());

                    xs.Serialize(writer, PhotoSessionCollection);
                    writer.Close();
                }
            }
        }

        public void Load()
        {
            using (IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //isoStorage.DeleteFile(DataFile);
                TextReader reader = null;
                IsolatedStorageFileStream file = null;
                try
                {
                    if (isoStorage.FileExists(DataFile))
                    {
                        file = isoStorage.OpenFile(DataFile, FileMode.Open);
                        
                        reader = new StreamReader(file);
                        XmlSerializer xs = new XmlSerializer(PhotoSessionCollection.GetType());
                        PhotoSessionCollection = ((ObservableCollection<PhotoSession>)xs.Deserialize(reader));
                    }
                }
                catch (InvalidOperationException exception)
                {
                    file.Position = 0;
                    reader = new StreamReader(file);
                    string test = reader.ReadToEnd();
                    file.Close();
                    file = null;
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

            }
        }

        public PhotoSession GetSession(uint id)
        {
            foreach (PhotoSession session in PhotoSessionCollection)
            {
                if (session.Id == id)
                {
                    return session;
                }
            }
            return null;
        }

        public PhotoSession CreateSession()
        {
            uint nextId = PhotoSessionCollection.Count == 0 ? 0 : PhotoSessionCollection[PhotoSessionCollection.Count - 1].Id + 1;
            return new PhotoSession(nextId, DateTime.Now);
        }
        

    }
    
}
