using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Html.Attributes.Variable.Integer;
using CtrlForm2.Html.Attributes.Variable.String;

namespace CtrlForm2.Html.Content.Elements.Input
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

        protected override string Prefix
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
