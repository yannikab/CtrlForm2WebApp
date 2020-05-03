using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
{
    public class AttrChecked : BooleanAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "checked"; }
        }

        #endregion


        #region Constructors

        public AttrChecked(bool? value)
            : base(value)
        {
        }

        public AttrChecked()
            : this(null)
        {
        }

        #endregion
    }
}
