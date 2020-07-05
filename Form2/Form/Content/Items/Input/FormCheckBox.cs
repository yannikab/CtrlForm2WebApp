﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Enums;
using Form2.Form.Interfaces;

namespace Form2.Form.Content.Items.Input
{
    public class FormCheckBox : FormInput<bool, bool>, IValidate<bool>
    {
        #region Fields

        private Func<bool, string> validator;

        private Action<bool> actionInvalid;

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


        #region IValidate<bool>

        public Func<bool, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<bool> ActionInvalid
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

                return string.IsNullOrEmpty(ValidationMessage);
            }
        }

        #endregion


        #region Constructors

        public FormCheckBox(string baseId, string formId)
            : base(baseId, formId)
        {
            Content = false;

            Validator = (v) => { return ""; };
            ActionInvalid = (v) => { return; };
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
