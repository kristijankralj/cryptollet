using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cryptollet.Common.Base
{
    public abstract class ExtendedBindableObject : BindableObject
    {
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

    public abstract class BaseViewModel : ExtendedBindableObject
    {
        public virtual Task InitializeAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (SetProperty(ref _isBusy, value))
                {
                    IsNotBusy = !_isBusy;
                }
            }
        }

        private bool _isNotBusy = true;
        public bool IsNotBusy
        {
            get => _isNotBusy;
            set
            {
                if (SetProperty(ref _isNotBusy, value))
                {
                    IsBusy = !_isNotBusy;
                }
            }
        }
    }
}
