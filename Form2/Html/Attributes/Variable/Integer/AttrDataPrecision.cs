using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Integer
{
    public class AttrDataPrecision : IntegerAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "data-precision"; }
        }

        #endregion


        #region Constructors 

        public AttrDataPrecision(long precision)
            : base(precision)
        {
        }

        public AttrDataPrecision()
            : base()
        {
        }

        #endregion
    }
}
