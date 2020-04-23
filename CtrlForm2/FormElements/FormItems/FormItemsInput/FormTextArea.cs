using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.FormElements.FormItems.FormItemsInput
{
    public class FormTextArea : FormTextBox
    {
        #region Fields

        private int rows;

        private int columns;

        #endregion


        #region Properties

        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }

        public int Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        #endregion


        #region Constructors

        public FormTextArea(string baseId, string formId)
            : base(baseId, formId)
        {
            rows = 4;
            columns = 50;
        }

        public FormTextArea(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion
    }
}
