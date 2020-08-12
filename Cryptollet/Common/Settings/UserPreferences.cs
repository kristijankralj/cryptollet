using System;
using Xamarin.Essentials;

namespace Cryptollet.Common.Settings
{
    public interface IUserPreferences
    {
        bool ContainsKey(string key);
        void Set(string key, bool value);
        bool Get(string key, bool defaultValue);
    }

    /// <summary>
    /// The wrapper class around the Xamarin.Essentials.Preferences class.
    /// So that unit testing is possible.
    /// </summary>
    public class UserPreferences : IUserPreferences
    {
        public bool ContainsKey(string key)
        {
            return Preferences.ContainsKey(key);
        }

        public bool Get(string key, bool defaultValue)
        {
            return Preferences.Get(key, defaultValue);
        }

        public void Set(string key, bool value)
        {
            Preferences.Set(key, value);
        }
    }
}
