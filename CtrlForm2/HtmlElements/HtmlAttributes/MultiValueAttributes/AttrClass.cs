using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
{
    public class AttrClass : HtmlMultiValueAttribute<string>
    {
        #region Properties

        public override string Name
        {
            get { return "class"; }
        }

        #endregion


        #region Constructors

        public AttrClass(IEnumerable<string> values)
            : base(values)
        {
        }

        public AttrClass()
            : base(Enumerable.Empty<string>())
        {
        }

        #endregion
    }
}
