using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cryptollet.Common.Base
{
    public abstract class BaseViewModel : BindableObject
    {
        public virtual Task InitializeAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
                          [CallerMemberName] string propertyName = "",
                          Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }
            backingStore = value;
            onChanged?.Invoke();

            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
