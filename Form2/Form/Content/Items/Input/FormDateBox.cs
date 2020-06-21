using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Interfaces;

namespace Form2.Form.Content.Items.Input
{
    [SuppressMessage("Style", "IDE0016:Use 'throw' expression", Justification = "<Pending>")]
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]

    public class FormDateBox : FormInput<string, DateTime?>, IReadOnly, IValidate<FormDateBox>
    {
        #region Fields

        private bool? isReadOnly;

        private Func<FormDateBox, string> validator;

        private Action<FormDateBox> actionInvalid;

        #endregion


        #region Properties

        public override DateTime? Value
        {
            get { try { return Convert.ToDateTime(Content); } catch { return null; }; }
        }

        #endregion


        #region IRequired

        public override bool? IsRequired
        {
            get
            {
                if (IsReadOnly ?? false)
                    return false;

                return base.IsRequired;
            }
            set
            {
                base.IsRequired = value;
            }
        }

        public override bool IsRequiredMet
        {
            get
            {
                if (IsHidden ?? false)
                    return true;

                if (IsDisabled ?? false)
                    return true;

                if (IsReadOnly ?? false)
                    return true;

                if (!(IsRequired ?? false))
                    return true;

                return Value.HasValue;
            }
        }

        #endregion


        #region IReadOnly

        public bool? IsReadOnly
        {
            get
            {
                // a user can not be expected to fill out an input element that is disabled
                if (IsDisabled ?? false)
                    return false;

                // a user can not be expected to fill out an input element that is hidden
                if (IsHidden ?? false)
                    return false;

                if (isReadOnly.HasValue)
                    return isReadOnly.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return null;

                return container.IsReadOnly;
            }
            set
            {
                isReadOnly = value;
            }
        }

        #endregion


        #region IValidate<FormDateBox>

        public Func<FormDateBox, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<FormDateBox> ActionInvalid
        {
            get { return actionInvalid; }
            set { actionInvalid = value; }
        }

        public string ValidationMessage
        {
            get { return Validator(this); }
        }

        public bool IsValid
        {
            get
            {
                // disabled elements are not submitted, it does not make sense to validate them
                if (IsDisabled ?? false)
                    return true;

                // a user can not edit hidden elements, it is unfair for them to participate in validation
                if (IsHidden ?? false)
                    return true;

                // a user can not edit readonly elements, it is unfair for them to participate in validation
                if (IsReadOnly ?? false)
                    return true;

                return !IsRequiredMet ? (!(IsRequired ?? false)) : string.IsNullOrEmpty(ValidationMessage);
            }
        }

        #endregion


        #region Constructors

        public FormDateBox(string baseId, string formId)
            : base(baseId, formId)
        {
            Content = "";

            IsReadOnly = null;

            Validator = (f) => { return ""; };
            ActionInvalid = (f) => { return; };
        }

        public FormDateBox(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0} (BaseId: '{1}', Label: '{2}', Value: {3})", GetType().Name, BaseId, Label, Value);
        }

        #endregion
    }
}
