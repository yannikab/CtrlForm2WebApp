using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
{
    public class AttrReadOnly : BooleanAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "readonly"; }
        }

        #endregion


        #region Constructors

        public AttrReadOnly(bool? value)
            : base(value)
        {
        }

        public AttrReadOnly()
            : this(null)
        {
        }

        #endregion
    }
}
