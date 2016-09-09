using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Prism.Mvvm;

namespace PowerPuff.Common
{
    public abstract class ValidatableBase: BindableBase, INotifyDataErrorInfo
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private readonly ErrorsContainer<string> _errors;

        protected ValidatableBase()
        {
            _errors = new ErrorsContainer<string>(NotifyErrorsChanged);
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _errors.GetErrors(propertyName);
        }

        public bool HasErrors => _errors.HasErrors;

        public void SetErrors(string propertyName, IEnumerable<string> errors)
        {
            _errors.SetErrors(propertyName, errors);
        }

        public void ClearErrors(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                _errors.ClearErrors(propertyName);
            }
        }

        public abstract bool IsValid();

        private void NotifyErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(() => HasErrors);
        }
    }
}
