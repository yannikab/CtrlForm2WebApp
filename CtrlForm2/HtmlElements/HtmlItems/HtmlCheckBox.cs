using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.HtmlElements.HtmlAttributes;

namespace UserControls.CtrlForm2.HtmlElements.HtmlItems
{
    public class HtmlCheckBox : HtmlInput
    {
        #region Fields

        private readonly AttrChecked attrChecked;

        #endregion


        #region Properties

        protected override string IdPrefix
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
