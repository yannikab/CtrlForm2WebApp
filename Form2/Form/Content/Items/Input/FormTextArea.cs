using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Form.Content.Items.Input
{
    public class FormTextArea : FormTextBox
    {
        #region Fields

        private int? rows;

        private int? columns;

        #endregion


        #region Properties

        public int? Rows
        {
            get { return rows; }
            set
            {
                if (value != null && value < 1)
                    throw new ArgumentException();

                rows = value;
            }
        }

        public int? Columns
        {
            get { return columns; }
            set
            {
                if (value != null && value < 1)
                    throw new ArgumentException();

                columns = value;
            }
        }

        #endregion


        #region Constructors

        public FormTextArea(string baseId, string formId)
            : base(baseId, formId)
        {
            rows = null;
            columns = null;
        }

        public FormTextArea(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion
    }
}
