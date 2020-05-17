using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Interfaces;
using Form2.Form.Selectables;

namespace Form2.Form.Content.Items.Input.Selectors
{
    public class FormRadioGroup : FormSelector<FormRadioButton, FormRadioButton>, IValidate<FormRadioGroup>
    {
        #region Fields

        private Func<FormRadioGroup, string> validator;

        private Action<FormRadioGroup> actionInvalid;

        #endregion


        #region Properties

        public override bool IsMultiSelect
        {
            get { return false; }
        }

        public override FormRadioButton Value
        {
            get { return Content.SingleOrDefault(c => c.IsSelected); }
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

                return Value != null;
            }
        }

        #endregion


        #region IValidate<FormRadioGroup>

        public Func<FormRadioGroup, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<FormRadioGroup> ActionInvalid
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

                return !IsRequiredMet ? (!(IsRequired ?? false)) : string.IsNullOrEmpty(ValidationMessage);
            }
        }

        #endregion


        #region Constructors

        public FormRadioGroup(string baseId, string formId)
            : base(baseId, formId)
        {
            Validator = (f) => { return ""; };
            ActionInvalid = (f) => { return; };
        }

        public FormRadioGroup(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0} (BaseId: '{1}', Value: '{2}')", GetType().Name, BaseId, Value != null ? Value.Value : "");
        }

        #endregion
    }
}
