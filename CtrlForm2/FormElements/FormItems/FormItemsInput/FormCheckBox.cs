using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.FormElements.FormItems.FormItemsInput
{
    public class FormCheckBox : FormItemInput
    {
        #region Fields

        private bool isChecked;

        #endregion


        #region Properties

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        #endregion


        #region Constructors

        public FormCheckBox(string baseId, string formId)
            : base(baseId, formId)
        {
        }

        public FormCheckBox(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}: {1}, Label: {2}, IsChecked: {3}", GetType().Name, BaseId, Label, IsChecked);
        }
    }
}
