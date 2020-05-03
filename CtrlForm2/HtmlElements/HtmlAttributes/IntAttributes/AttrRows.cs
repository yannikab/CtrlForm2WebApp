using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
{
    public class AttrRows : IntAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "rows"; }
        }

        #endregion


        #region Constructors 

        public AttrRows(int rows)
            : base(rows)
        {
        }

        #endregion
    }
}
