using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
{
    public class AttrValue : HtmlAttribute<string>
    {
        #region Properties

        public override string Name
        {
            get { return "value"; }
        }

        #endregion


        #region Constructors

        public AttrValue(string value)
            : base(value)
        {
        }

        public AttrValue()
            : this(null)
        {
        }

        #endregion
    }
}
