using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Decimal
{
    public class AttrDataMax : DecimalAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "data-max"; }
        }

        #endregion


        #region Constructors 

        public AttrDataMax(decimal max)
            : base(max)
        {
        }

        public AttrDataMax()
            : base()
        {
        }

        #endregion
    }
}
