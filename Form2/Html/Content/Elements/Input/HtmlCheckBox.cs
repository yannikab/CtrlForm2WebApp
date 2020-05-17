using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Attributes.Variable.Boolean;

namespace Form2.Html.Content.Elements.Input
{
    public class HtmlCheckBox : HtmlInput
    {
        #region Fields

        private readonly AttrChecked attrChecked;

        #endregion


        #region Properties

        protected override string Prefix
        {
            get { return "cbx"; }
        }

        public AttrChecked Checked
        {
            get { return attrChecked; }
        }

        #endregion


        #region Constructors

        public HtmlCheckBox(string baseId)
            : base(baseId, "checkbox")
        {
            attributes.Add(attrChecked = new AttrChecked());
        }

        #endregion
    }
}
