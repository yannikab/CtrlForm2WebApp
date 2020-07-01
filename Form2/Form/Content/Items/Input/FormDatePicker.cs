using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Enums;
using Form2.Form.Interfaces;

namespace Form2.Form.Content.Items.Input
{
    [SuppressMessage("Style", "IDE0016:Use 'throw' expression", Justification = "<Pending>")]
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]

    public class FormDatePicker : FormInput<string, DateTime>, IReadOnly, IValidate<FormDatePicker>
    {
        #region Fields

        private string placeHolder;

        private FormIcon icon;

        private bool? readOnly;

        private Func<FormDatePicker, string> validator;

        private Action<FormDatePicker> actionInvalid;

        private string dateFormat;

        #endregion


        #region Properties

        public string PlaceHolder
        {
            get { return placeHolder; }
            set { placeHolder = value; }
        }

        public FormIcon Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public string DateFormat
        {
            get { return dateFormat; }
            set { dateFormat = value; }
        }

        public override DateTime Value
        {
            get { try { return DateTime.ParseExact(Content, dateFormat.Replace('m', 'M'), CultureInfo.InvariantCulture); } catch { return DateTime.MinValue; }; }
        }

        public override bool HasValue
        {
            get { try { DateTime.ParseExact(Content, dateFormat.Replace('m', 'M'), CultureInfo.InvariantCulture); return true; } catch { return false; }; }
        }

        #endregion


        #region IRequired

        public override bool? Required
        {
            set { base.Required = value; }
        }

        public override bool IsRequired
        {
            get
            {
                if (IsReadOnly)
                    return false;

                return base.IsRequired;
            }
        }

        public override bool IsRequiredMet
        {
            get
            {
                if (IsHidden)
                    return true;

                if (IsDisabled)
                    return true;

                if (IsReadOnly)
                    return true;

                if (!IsRequired)
                    return true;

                return HasValue;
            }
        }

        #endregion


        #region IReadOnly

        public bool? ReadOnly
        {
            set { readOnly = value; }
        }

        public bool IsReadOnly
        {
            get
            {
                // a user can not be expected to fill out an input element that is disabled
                if (IsDisabled)
                    return false;

                // a user can not be expected to fill out an input element that is hidden
                if (IsHidden)
                    return false;

                if (readOnly.HasValue)
                    return readOnly.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return false;

                return container.IsReadOnly;
            }
        }

        #endregion


        #region IValidate<FormDatePicker>

        public Func<FormDatePicker, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<FormDatePicker> ActionInvalid
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
                if (IsDisabled)
                    return true;

                // a user can not edit hidden elements, it is unfair for them to participate in validation
                if (IsHidden)
                    return true;

                // a user can not edit readonly elements, it is unfair for them to participate in validation
                if (IsReadOnly)
                    return true;

                return !IsRequiredMet ? !IsRequired : string.IsNullOrEmpty(ValidationMessage);
            }
        }

        #endregion


        #region Constructors

        public FormDatePicker(string baseId, string formId, string dateFormat)
            : base(baseId, formId)
        {
            Content = "";
            PlaceHolder = "";
            Icon = FormIcon.Calendar;

            DateFormat = dateFormat;

            readOnly = null;

            Validator = (f) => { return ""; };
            ActionInvalid = (f) => { return; };
        }

        public FormDatePicker(string baseId, string dateFormat)
            : this(baseId, baseId.ToLower(), dateFormat)
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
