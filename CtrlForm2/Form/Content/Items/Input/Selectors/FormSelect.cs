using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CtrlForm2.Form.Selectables;

namespace CtrlForm2.Form.Content.Items.Input.Selectors
{
    public class FormSelect : FormSelector<FormOption>
    {
        #region Fields

        private readonly bool isMultiSelect;

        private readonly int? size;

        private FormOption header;

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

        public override IEnumerable<FormOption> Options
        {
            set
            {
                base.Options = value;

                if (header != null)
                    Insert(0, header);
            }
        }

        public override bool IsEntered
        {
            get
            {
                var options = Options;

                if (header != null)
                    options = options.Except(new FormOption[] { header });

                return options.Any(o => o.IsSelected);
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
            this.header = null;
        }

        public FormSelect(string baseId, string formId, bool multiSelect)
            : base(baseId, formId)
        {
            this.isMultiSelect = multiSelect;
            this.size = null;
            this.header = null;
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
    }
}
