using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Html.Attributes.ReadOnly.String;
using CtrlForm2.Html.Attributes.Variable.Boolean;
using CtrlForm2.Html.Attributes.Variable.String;

namespace CtrlForm2.Html.Content.Elements.Input
{
    public class HtmlRadioButton : HtmlInput
    {
        #region Fields

        private readonly AttrChecked attrChecked;

        #endregion


        #region Properties

        protected override string Prefix
        {
            get { return "rbt"; }
        }

        public AttrChecked Checked
        {
            get { return attrChecked; }
        }

        #endregion


        #region Constructors

        public HtmlRadioButton(string name, string value)
            : base(string.Format("{0}{1}", name, value), name, "radio")
        {
            attributes.Add(attrChecked = new AttrChecked());
        }

        #endregion
    }
}
