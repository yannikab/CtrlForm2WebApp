using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Form.Content.Items.Input
{
    public class FormCheckBox : FormInput
    {
        #region Fields

        private bool isChecked;

        private string textChecked;

        private string textNotChecked;

        #endregion


        #region Properties

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        public string TextChecked
        {
            get { return textChecked; }
            set { textChecked = value; }
        }

        public string TextNotChecked
        {
            get { return textNotChecked; }
            set { textNotChecked = value; }
        }

        #endregion


        #region IRequired

        public override bool IsEntered
        {
            get { return isChecked; }
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


        #region Object

        public override string ToString()
        {
            return string.Format("{0}: {1}, Label: {2}, IsChecked: {3}", GetType().Name, BaseId, Label, IsChecked);
        }

        #endregion
    }
}
