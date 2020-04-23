using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlGroups
{
    public class HtmlDiv : HtmlGroup
    {
        #region Properties

        public override string Tag
        {
            get { return "div"; }
        }

        protected override string IdPrefix
        {
            get { return "div"; }
        }

        #endregion


        #region Constructors

        public HtmlDiv(string baseId)
            : base(baseId)
        {
        }

        public HtmlDiv()
            : this("")
        {
        }

        #endregion
    }
}
