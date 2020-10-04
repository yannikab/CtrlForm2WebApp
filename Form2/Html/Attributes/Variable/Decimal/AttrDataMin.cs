using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Decimal
{
    public class AttrDataMin : DecimalAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "data-min"; }
        }

        #endregion


        #region Constructors 

        public AttrDataMin(decimal min)
            : base(min)
        {
        }

        public AttrDataMin()
            : base()
        {
        }

        #endregion
    }
}
