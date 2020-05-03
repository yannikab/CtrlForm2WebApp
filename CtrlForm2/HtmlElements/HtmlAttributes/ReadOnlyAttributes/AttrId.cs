using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
{
    public class AttrId : HtmlReadOnlyAttribute<string>
    {
        #region Properties

        public override string Name
        {
            get { return "id"; }
        }

        #endregion


        #region Constructors

        public AttrId(string value)
            : base(value)
        {
        }

        public AttrId()
            : this(null)
        {
        }

        #endregion
    }
}
