using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Enums;
using Form2.Form.Interfaces;
using Form2.Form.Selectables;

namespace Form2.Form.Content.Items.Input.Selectors
{
    public class FormRadioGroup : FormSelector<FormRadioButton, FormRadioButton>, IValidate<FormRadioButton>
    {
        #region Fields

        private Func<FormRadioButton, string> validator;

        private Action<FormRadioButton> actionInvalid;

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

        public override bool HasValue
        {
            get { return Value != null; }
        }

        #endregion


        #region IValidate<FormRadioButton>

        public Func<FormRadioButton, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<FormRadioButton> ActionInvalid
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

                return HasValue ? string.IsNullOrEmpty(ValidationMessage) : !IsRequired;
            }
        }

        #endregion


        #region Constructors

        public FormRadioGroup(string baseId, string formId)
            : base(baseId, formId)
        {
            Validator = (v) => { return ""; };
            ActionInvalid = (v) => { return; };
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
