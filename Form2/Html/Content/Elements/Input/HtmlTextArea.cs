using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Attributes.Variable.Integer;
using Form2.Html.Attributes.Variable.String;

namespace Form2.Html.Content.Elements.Input
{
    public class HtmlTextArea : HtmlInput
    {
        #region Fields

        private readonly AttrPlaceholder attrPlaceholder;

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

        public AttrPlaceholder Placeholder
        {
            get { return attrPlaceholder; }
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

        public HtmlTextArea(string name)
            : base(name, null)
        {
            attributes.Add(attrPlaceholder = new AttrPlaceholder());

            attributes.Add(attrRows = new AttrRows());

            attributes.Add(attrCols = new AttrCols());
        }

        #endregion
    }
}
