using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlGroups
{
    public class HtmlSpan : HtmlContainer
    {
        #region Properties

        public override string Tag
        {
            get { return "span"; }
        }

        protected override string IdPrefix
        {
            get { return "spn"; }
        }

        #endregion


        #region Constructors

        public HtmlSpan(string baseId)
            : base(baseId)
        {
        }

        public HtmlSpan()
            : this("")
        {
        }

        #endregion
    }
}
