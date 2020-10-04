using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Decimal
{
    public class AttrDataStep : DecimalAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "data-step"; }
        }

        #endregion


        #region Constructors 

        public AttrDataStep(decimal step)
            : base(step)
        {
        }

        public AttrDataStep()
            : base()
        {
        }

        #endregion
    }
}
