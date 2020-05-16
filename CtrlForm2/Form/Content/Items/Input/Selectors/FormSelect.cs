using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CtrlForm2.Form.Interfaces;
using CtrlForm2.Form.Selectables;

namespace CtrlForm2.Form.Content.Items.Input.Selectors
{
    public class FormSelect : FormSelector<FormOption, IEnumerable<FormOption>>, IValidate<FormSelect>
    {
        #region Fields

        private readonly bool isMultiSelect;

        private readonly int? size;

        private FormOption header;

        private Func<FormSelect, string> validator;

        private Action<FormSelect> actionInvalid;

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

                return Value.Any();
            }
        }

        #endregion


        #region IValidate<FormSelect>

        public Func<FormSelect, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<FormSelect> ActionInvalid
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

        public FormSelect(string baseId, string formId, int size)
            : base(baseId, formId)
        {
            if (size < 1)
                throw new ArgumentException();

            this.isMultiSelect = true;
            this.size = size;
            Header = null;

            Validator = (f) => { return ""; };
            ActionInvalid = (f) => { return; };
        }

        public FormSelect(string baseId, string formId, bool multiSelect)
            : base(baseId, formId)
        {
            this.isMultiSelect = multiSelect;
            this.size = null;
            Header = null;

            Validator = (f) => { return ""; };
            ActionInvalid = (f) => { return; };
        }

        public FormSelect(string baseId, int size)
            : this(baseId, baseId.ToLower(), size)
        {
        }

        public FormSelect(string baseId, bool multiSelect)
            : this(baseId, baseId.ToLower(), multiSelect)
        {
        }

        #endregion


        #region Object

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("{0} (BaseId: '{1}', Value: [", GetType().Name, BaseId));

            int i = 0;
            foreach (var c in Content.Where(c => c.IsSelected))
                sb.Append(string.Format("{0}'{1}'", i++ == 0 ? "" : ", ", c.Value));

            sb.Append("])");

            return sb.ToString();
        }

        #endregion
    }
}
