using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Form.Content.Items.Input
{
    public class FormTextArea : FormTextBox
    {
        #region Fields

        private static readonly int defaultRows;

        private static readonly int defaultColumns;

        private int rows;

        private int columns;

        #endregion


        #region Properties

        public int Rows
        {
            get { return rows; }
            set
            {
                if (value < 1)
                    throw new ArgumentException();

                rows = value;
            }
        }

        public int Columns
        {
            get { return columns; }
            set
            {
                if (value < 1)
                    throw new ArgumentException();

                columns = value;
            }
        }

        #endregion


        #region Constructors

        static FormTextArea()
        {
            defaultRows = 4;
            defaultColumns = 50;
        }

        public FormTextArea(string baseId, string formId)
            : base(baseId, formId)
        {
            Rows = defaultRows;
            Columns = defaultColumns;
        }

        public FormTextArea(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion
    }
}
