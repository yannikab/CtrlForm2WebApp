using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Interfaces;

namespace Form2.Form.Content.Items.Input
{
    public class FormCheckBox : FormInput<bool, bool>
    {
        #region Properties

        public override bool Value
        {
            get { return Content; }
        }

        public override bool HasValue
        {
            get { return true; }
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

                return ValidationMessage == null;
            }
        }

        public override bool IsDisabled
        {
            get
            {
                if (disabled.HasValue)
                    return disabled.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return false;

                return container.IsDisabled || container.IsReadOnly;
            }
        }

        #endregion


        #region Constructors

        public FormCheckBox(string name)
            : base(name)
        {
            Content = false;
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0} (Path: '{1}', Label: '{2}', Value: {3})", GetType().Name, Path, Label, Value);
        }

        #endregion
    }
}
