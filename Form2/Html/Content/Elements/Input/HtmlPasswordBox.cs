using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Attributes.Variable.String;

namespace Form2.Html.Content.Elements.Input
{
    public class HtmlPasswordBox : HtmlInput
    {
        #region Fields

        private readonly AttrPlaceholder attrPlaceholder;

        #endregion


        #region Properties

        protected override string Prefix
        {
            get { return "txt"; }
        }

        public AttrPlaceholder Placeholder
        {
            get { return attrPlaceholder; }
        }

        #endregion


        #region Constructors

        public HtmlPasswordBox(string baseId)
            : base(baseId, "password")
        {
            attributes.Add(attrPlaceholder = new AttrPlaceholder());
        }

        #endregion
    }
}
