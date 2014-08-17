using System;
using System.ComponentModel;

namespace HighEnergy.Collections
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void Set<T>(string propertyName, ref T backingField, T newValue, Action beforeChange = null, Action afterChange = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("propertyName");

            if (backingField == null && newValue == null)
                return;

            if (backingField != null && backingField.Equals(newValue))
                return;

            if (beforeChange != null)
                beforeChange();

            backingField = newValue;

            OnPropertyChanged(propertyName);

            if (afterChange != null)
                afterChange();
        }
    }
}