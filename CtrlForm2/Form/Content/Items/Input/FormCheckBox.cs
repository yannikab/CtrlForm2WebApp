using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Interfaces;

namespace CtrlForm2.Form.Content.Items.Input
{
    public enum CheckBoxState
    {
        Off,
        On
    }

    public class FormCheckBox : FormInput<CheckBoxState?, bool>, IValidate<FormCheckBox>
    {
        #region Fields

        private Func<FormCheckBox, string> validator;

        private Action<FormCheckBox> actionInvalid;

        #endregion


        #region Properties

        public override bool Value
        {
            get { return Content == CheckBoxState.On; }
        }

        #endregion


        #region IRequired

        public override bool IsRequiredMet
        {
            get
            {
                if (IsHidden ?? false)
                    return true;

                if (IsDisabled ?? false)
                    return true;

                if (!(IsRequired ?? false))
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
                if (IsDisabled ?? false)
                    return true;

                // a user can not edit hidden elements, it is unfair for them to participate in validation
                if (IsHidden ?? false)
                    return true;

                return string.IsNullOrEmpty(ValidationMessage);
            }
        }

        #endregion


        #region Constructors

        public FormCheckBox(string baseId, string formId)
            : base(baseId, formId)
        {
            Content = CheckBoxState.Off;

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
