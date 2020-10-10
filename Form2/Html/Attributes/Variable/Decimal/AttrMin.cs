using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Decimal
{
    public class AttrMin : DecimalAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "min"; }
        }

        #endregion


        #region Constructors 

        public AttrMin(long min)
            : base(min)
        {
        }

        public AttrMin()
            : base()
        {
        }

        #endregion
    }
}
