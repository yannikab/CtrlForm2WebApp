﻿using System;
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

    public class FormNumberBox : FormInput<string, decimal>, IReadOnly, IValidate<decimal>
    {
        #region Fields

        private string placeholder;

        private decimal? min;

        private decimal? max;

        private decimal? step;

        private FormIcon icon;

        private bool? readOnly;

        private Func<decimal, string> validator;

        private Action<decimal> actionInvalid;

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

        public decimal? Min
        {
            get { return min; }
            set { min = value; }
        }

        public decimal? Max
        {
            get { return max; }
            set { max = value; }
        }

        public decimal? Step
        {
            get { return step; }
            set { step = value; }
        }

        public override decimal Value
        {
            get { try { return decimal.Parse(Content); } catch { return decimal.MinValue; }; }
        }

        public override bool HasValue
        {
            get { return Value != decimal.MinValue; }
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

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return false;

                return container.IsReadOnly;
            }
        }

        #endregion


        #region IValidate<decimal>

        public Func<decimal, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<decimal> ActionInvalid
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

        public FormNumberBox(string name)
            : base(name)
        {
            Content = "0";
            placeholder = "";

            min = null;
            max = null;
            step = null;

            Icon = FormIcon.NotSet;

            readOnly = null;

            validator = (v) => { return null; };
            actionInvalid = (v) => { return; };
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0} (Path: '{1}', Label: '{2}', Value: '{3}')", GetType().Name, Path, Label, Value);
        }

        #endregion
    }
}
