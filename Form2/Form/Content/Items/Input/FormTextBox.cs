using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Enums;
using Form2.Form.Interfaces;

namespace Form2.Form.Content.Items.Input
{
    [SuppressMessage("Style", "IDE0016:Use 'throw' expression", Justification = "<Pending>")]
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]

    public class FormTextBox : FormInput<string, string>, IReadOnly, IValidate<string>
    {
        #region Fields

        private string placeholder;

        private FormIcon icon;

        private bool? readOnly;

        private Func<string, string> validator;

        private Action<string> actionInvalid;

        #endregion


        #region Properties

        public string Placeholder
        {
            get { return placeholder; }
            set { placeholder = value; }
        }

        public FormIcon Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public override string Value
        {
            get { return Content.Trim(); }
        }

        public override bool HasValue
        {
            get { return !string.IsNullOrWhiteSpace(Value); }
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


        #region IValidate<string>

        public Func<string, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<string> ActionInvalid
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

                return HasValue ? ValidationMessage == null : !IsRequired;
            }
        }

        #endregion


        #region Constructors

        public FormTextBox(string baseId, string formId)
            : base(baseId, formId)
        {
            Content = "";
            placeholder = "";
            Icon = FormIcon.NotSet;

            readOnly = null;

            validator = (v) => { return null; };
            actionInvalid = (v) => { return; };
        }

        public FormTextBox(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0} (BaseId: '{1}', Label: '{2}', Value: '{3}')", GetType().Name, BaseId, Label, Value);
        }

        #endregion
    }
}
