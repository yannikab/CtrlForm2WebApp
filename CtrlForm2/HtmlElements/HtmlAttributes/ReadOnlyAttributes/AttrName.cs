using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
{
    public class AttrName : HtmlReadOnlyAttribute<string>
    {
        #region Properties

        public override string Name
        {
            get { return "name"; }
        }

        #endregion


        #region Constructors

        public AttrName(string value)
            : base(value)
        {
        }

        public AttrName()
            : this(null)
        {
        }

        #endregion
    }
}
