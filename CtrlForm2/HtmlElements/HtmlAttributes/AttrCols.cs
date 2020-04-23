using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
{
    public class AttrCols : HtmlAttribute<int>
    {
        #region Properties

        public override string Name
        {
            get { return "cols"; }
        }

        #endregion


        #region Constructors 

        public AttrCols(int cols)
            : base(cols)
        {
        }

        #endregion
    }
}
