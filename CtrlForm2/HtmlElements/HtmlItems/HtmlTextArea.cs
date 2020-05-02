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

        private readonly AttrPlaceHolder attrPlaceHolder;

        private readonly AttrRows attrRows;

        private readonly AttrCols attrCols;

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
            get { return attrPlaceHolder; }
        }

        public AttrRows Rows
        {
            get { return attrRows; }
        }

        public AttrCols Cols
        {
            get { return attrCols; }
        }

        #endregion


        #region Constructors

        public HtmlTextArea(string baseId, int rows, int cols)
            : base(baseId, null)
        {
            attributes.Add(attrPlaceHolder = new AttrPlaceHolder());

            attributes.Add(attrRows = new AttrRows(rows));

            attributes.Add(attrCols = new AttrCols(cols));
        }

        #endregion
    }
}
