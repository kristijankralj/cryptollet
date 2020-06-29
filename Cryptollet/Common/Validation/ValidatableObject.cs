using System;
using System.Collections.Generic;
using System.Linq;
using Cryptollet.Common.Base;

namespace Cryptollet.Common.Validation
{
    public class ValidatableObject<T> : ExtendedBindableObject
    {
        public List<IValidationRule<T>> Validations { get; }

        private List<string> _errors;
        public List<string> Errors
        {
            get => _errors;
            set { SetProperty(ref _errors, value); }
        }

        private T _value;
        public T Value
        {
            get => _value;
            set { SetProperty(ref _value, value); }
        }

        private bool _isValid;
        public bool IsValid
        {
            get => _isValid;
            set { SetProperty(ref _isValid, value); }
        }

        public ValidatableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            Validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return IsValid;
        }
    }
}
