using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Enums;
using Form2.Form.Interfaces;

namespace Form2.Form.Content.Items.Input
{
    public class FormCheckBox : FormInput<bool, bool>, IValidate<FormCheckBox>
    {
        #region Fields

        private Func<FormCheckBox, string> validator;

        private Action<FormCheckBox> actionInvalid;

        #endregion


        #region Properties

        public override bool Value
        {
            get { return Content; }
        }

        public override bool HasValue
        {
            get { return true; }
        }

        #endregion


        #region IRequired

        public override bool IsRequiredMet
        {
            get
            {
                if (IsHidden)
                    return true;

                if (IsDisabled)
                    return true;

                if (!IsRequired)
                    return true;

                return Value;
            }
        }

        #endregion


        #region IValidate<FormTextBox>

        public Func<FormCheckBox, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<FormCheckBox> ActionInvalid
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

                return string.IsNullOrEmpty(ValidationMessage);
            }
        }

        #endregion


        #region Constructors

        public FormCheckBox(string baseId, string formId)
            : base(baseId, formId)
        {
            Content = false;

            Validator = (f) => { return ""; };
            ActionInvalid = (f) => { return; };
        }

        public FormCheckBox(string baseId)
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
