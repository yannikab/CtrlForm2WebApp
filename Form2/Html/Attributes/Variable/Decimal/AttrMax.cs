using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Decimal
{
    public class AttrMax : DecimalAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "max"; }
        }

        #endregion


        #region Constructors 

        public AttrMax(long max)
            : base(max)
        {
        }

        public AttrMax()
            : base()
        {
        }

        #endregion
    }
}
