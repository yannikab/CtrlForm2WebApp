using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.HtmlElements.HtmlAttributes;

namespace UserControls.CtrlForm2.HtmlElements.HtmlItems
{
    public class HtmlTextArea : HtmlInput
    {
        #region Fields

        private readonly AttrPlaceHolder placeHolder;

        private readonly AttrRows rows;

        private readonly AttrCols cols;

        #endregion


        #region Properties

        public override string Tag
        {
            get { return "textarea"; }
        }

        protected override string IdPrefix
        {
            get { return "txa"; }
        }

        public AttrPlaceHolder PlaceHolder
        {
            get { return placeHolder; }
        }

        public AttrRows Rows
        {
            get { return rows; }
        }

        public AttrCols Cols
        {
            get { return cols; }
        }

        #endregion


        #region Constructors

        public HtmlTextArea(string baseId, int rows, int cols)
            : base(baseId, null)
        {
            placeHolder = new AttrPlaceHolder();

            this.rows = new AttrRows(rows);

            this.cols = new AttrCols(cols);
        }

        #endregion
    }
}
