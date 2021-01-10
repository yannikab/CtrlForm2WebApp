using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Form2.Form.Selectables;

namespace Form2.Form.Content.Items.Input.Selectors
{
    public class FormSelect : FormSelector<FormOption, IEnumerable<FormOption>>
    {
        #region Fields

        private readonly bool multiSelect;

        private readonly int? size;

        private FormOption header;

        #endregion


        #region Properties

        public override bool IsMultiSelect
        {
            get { return multiSelect; }
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

        public FormSelect(string name, int size)
            : base(name)
        {
            if (size < 1)
                throw new ArgumentException();

            this.multiSelect = true;
            this.size = size;
            Header = null;
        }

        public FormSelect(string name, bool multiSelect)
            : base(name)
        {
            this.multiSelect = multiSelect;
            this.size = null;
            Header = null;
        }

        #endregion


        #region Object

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("{0} (Path: '{1}', Value: [", GetType().Name, Path));

            int i = 0;
            foreach (var c in Content.Where(c => c.IsSelected))
                sb.Append(string.Format("{0}'{1}'", i++ == 0 ? "" : ", ", c.Value));

            sb.Append("])");

            return sb.ToString();
        }

        #endregion
    }
}
