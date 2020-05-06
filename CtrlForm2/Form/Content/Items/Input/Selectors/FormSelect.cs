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

        #endregion


        #region Constructors

        public FormSelect(string baseId, string formId, int size)
            : base(baseId, formId)
        {
            if (size < 1)
                throw new ArgumentException();

            this.isMultiSelect = true;
            this.size = size;
        }

        public FormSelect(string baseId, string formId, bool multiSelect)
            : base(baseId, formId)
        {
            this.isMultiSelect = multiSelect;
            this.size = null;
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
