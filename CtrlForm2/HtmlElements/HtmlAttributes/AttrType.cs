using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
{
    public class AttrType : HtmlReadOnlyAttribute<string>
    {
        #region Properties

        public override string Name
        {
            get { return "type"; }
        }

        #endregion


        #region Constructors

        public AttrType(string value)
            : base(value)
        {
        }

        public AttrType()
            : this(null)
        {
        }

        #endregion
    }
}
