using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Form2.Form.Interfaces;
using Form2.Form.Selectables;

namespace Form2.Form.Content.Items.Input.Selectors
{
    public class FormSelect : FormSelector<FormOption, IEnumerable<FormOption>>, IValidate<IEnumerable<FormOption>>
    {
        #region Fields

        private readonly bool isMultiSelect;

        private readonly int? size;

        private FormOption header;

        private Func<IEnumerable<FormOption>, string> validator;

        private Action<IEnumerable<FormOption>> actionInvalid;

        #endregion


        #region Properties

        public override bool IsMultiSelect
        {
            get { return isMultiSelect; }
        }

        public int? Size
        {
            get { return size; }
        }

        public FormOption Header
        {
            get { return header; }
            set
            {
                if (value == header)
                    return;

                if (header != null && !Remove(header))
                    throw new ApplicationException();

                if (value != null)
                    Insert(0, value);

                header = value;
            }
        }

        public override IEnumerable<FormOption> Content
        {
            get { return base.Content; }
            set
            {
                base.Content = value;

                if (header != null)
                    Insert(0, header);
            }
        }

        public override IEnumerable<FormOption> Value
        {
            get
            {
                foreach (var c in Content)
                {
                    if (c == header)
                        continue;

                    if (c.IsSelected)
                        yield return c;
                }
            }
        }

        public override bool HasValue
        {
            get { return Value.Any(); }
        }

        #endregion


        #region IValidate<IEnumerable<FormOption>>

        public Func<IEnumerable<FormOption>, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<IEnumerable<FormOption>> ActionInvalid
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

        public FormSelect(string name, int size)
            : base(name)
        {
            if (size < 1)
                throw new ArgumentException();

            this.isMultiSelect = true;
            this.size = size;
            Header = null;

            validator = (v) => { return null; };
            actionInvalid = (v) => { return; };
        }

        public FormSelect(string name, bool multiSelect)
            : base(name)
        {
            this.isMultiSelect = multiSelect;
            this.size = null;
            Header = null;

            validator = (v) => { return null; };
            actionInvalid = (v) => { return; };
        }

        #endregion


        #region Object

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("{0} (Name: '{1}', Value: [", GetType().Name, Name));

            int i = 0;
            foreach (var c in Content.Where(c => c.IsSelected))
                sb.Append(string.Format("{0}'{1}'", i++ == 0 ? "" : ", ", c.Value));

            sb.Append("])");

            return sb.ToString();
        }

        #endregion
    }
}
