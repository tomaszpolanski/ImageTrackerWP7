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
using System.Collections;
using System.Collections.Generic;

namespace StateStorageLibrary
{
    public class StateStorage
    {
        public static void SaveState(object page, object state, string name)
        {
            IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;
            string dictionaryKey = CreateKey(page, name);
            storage[dictionaryKey] = state;
            storage.Save();
        }

        public static void ResetState(object page, string name)
        {
            string dictionaryKey = CreateKey(page, name);
            IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;
            storage.Remove(dictionaryKey);
        }

        public static void ResetAllStates(object page)
        {
            IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;
            ICollection collection = storage.Keys;
            string pageFormat = CreateKey(page, "");
            List<string> keysForRemoval = new List<string>();
            foreach (string key in collection)
            {
                if (key.StartsWith(pageFormat))
                {
                    keysForRemoval.Add(key);
                }
            }

            foreach (string key in keysForRemoval)
            {
                storage.Remove(key);
            }

        }

        public static object LoadState(object page, string name)
        {
            IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;
            string dictionaryKey = CreateKey( page, name);
            if (storage.Contains(dictionaryKey))
            {
                return storage[dictionaryKey];
            }
            return null;
        }

        private static string CreateKey(object parent, string name)
        {
            return String.Format("{0}_{1}", parent.GetType().ToString(), name);
        }
    }
}
