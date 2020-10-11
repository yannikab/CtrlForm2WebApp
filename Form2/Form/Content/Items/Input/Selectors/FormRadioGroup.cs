using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Selectables;

namespace Form2.Form.Content.Items.Input.Selectors
{
    public class FormRadioGroup : FormSelector<FormRadioButton, FormRadioButton>
    {
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

        public override bool IsValid
        {
            get
            {
                // a user can not edit hidden elements, it is unfair for them to participate in validation
                if (IsHidden)
                    return true;

                // disabled elements are not submitted, it does not make sense to validate them
                if (IsDisabled)
                    return true;

                return HasValue ? ValidationMessage == null : !IsRequired;
            }
        }

        #endregion


        #region Constructors

        public FormRadioGroup(string name)
            : base(name)
        {
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0} (Path: '{1}', Value: '{2}')", GetType().Name, Path, Value != null ? Value.Value : "");
        }

        #endregion
    }
}
