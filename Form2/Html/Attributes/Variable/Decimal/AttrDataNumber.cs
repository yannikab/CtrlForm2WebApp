using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Decimal
{
    public class AttrDataNumber : DecimalAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "data-number"; }
        }

        #endregion


        #region Constructors 

        public AttrDataNumber(decimal number)
            : base(number)
        {
        }

        public AttrDataNumber()
            : base()
        {
        }

        #endregion
    }
}
