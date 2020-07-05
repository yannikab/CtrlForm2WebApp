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

    public class FormDatePicker : FormInput<string, DateTime>, IReadOnly, IValidate<DateTime>
    {
        #region Fields

        private string placeHolder;

        private FormIcon icon;

        private bool? readOnly;

        private Func<DateTime, string> validator;

        private Action<DateTime> actionInvalid;

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
            get { return Value != DateTime.MinValue; }
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
                // a user can not be expected to fill out an input element that is hidden
                if (IsHidden)
                    return false;

                // a user can not be expected to fill out an input element that is disabled
                if (IsDisabled)
                    return false;

                if (readOnly.HasValue)
                    return readOnly.Value;

                FormSection container = Container as FormSection;

                if (container == null)
                    return false;

                return container.IsReadOnly;
            }
        }

        #endregion


        #region IValidate<FormDatePicker>

        public Func<DateTime, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<DateTime> ActionInvalid
        {
            get { return actionInvalid; }
            set { actionInvalid = value; }
        }

        public string ValidationMessage
        {
            get { return Validator(Value); }
        }

        public bool IsValid
        {
            get
            {
                // a user can not edit hidden elements, it is unfair for them to participate in validation
                if (IsHidden)
                    return true;

                // disabled elements are not submitted, it does not make sense to validate them
                if (IsDisabled)
                    return true;

                // a user can not edit readonly elements, it is unfair for them to participate in validation
                if (IsReadOnly)
                    return true;

                return HasValue ? string.IsNullOrEmpty(ValidationMessage) : !IsRequired;
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

            Validator = (v) => { return ""; };
            ActionInvalid = (v) => { return; };
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
