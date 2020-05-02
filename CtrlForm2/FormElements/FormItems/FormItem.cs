using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.FormElements.FormItems
{
    public abstract class FormItem
    {
        #region Fields

        private FormGroup group;

        private readonly string baseId;

        private readonly string formId;

        private bool? isHidden;

        #endregion


        #region Properties

        public FormGroup Group
        {
            get { return group; }
            set { group = value; }
        }

        public string BaseId
        {
            get { return baseId; }
        }

        public string FormId
        {
            get { return formId; }
        }

        public bool? IsHidden
        {
            get
            {
                if (isHidden.HasValue)
                    return isHidden.Value;

                if (group == null)
                    return null;

                return group.IsHidden;
            }

            set
            {
                isHidden = value;
            }
        }

        public int Depth
        {
            get { return group == null ? 0 : group.Depth + 1; }
        }

        #endregion


        #region Constructors

        public FormItem(string baseId, string formId)
        {
            if (string.IsNullOrEmpty(baseId))
                throw new ArgumentException();

            if (string.IsNullOrEmpty(formId))
                throw new ArgumentException();

            this.baseId = baseId;
            this.formId = formId;
        }

        #endregion
    }
}
