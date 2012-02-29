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
using System.IO.IsolatedStorage;

namespace StateStorageLibrary
{
    public class StateStorage
    {
        public static void SaveState(object page, object state, string name)
        {
            IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;
            string dictionaryKey = String.Format( "{0}_{1}", page.GetType().ToString(), name);
            storage[dictionaryKey] = state;
            storage.Save();
        }

        public static void ResetState(object page, string name)
        {
            string dictionaryKey = String.Format("{0}_{1}", page.GetType().ToString(), name);
            IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;
            storage.Remove(dictionaryKey);
        }

        public static object LoadState(object page, string name)
        {
            IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;
            string dictionaryKey = String.Format( "{0}_{1}", page.GetType().ToString(), name);
            if (storage.Contains(dictionaryKey))
            {
                return storage[dictionaryKey];
            }
            return null;
        }
    }
}
