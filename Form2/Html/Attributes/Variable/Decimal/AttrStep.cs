using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Decimal
{
    public class AttrStep : DecimalAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "step"; }
        }

        #endregion


        #region Constructors 

        public AttrStep(decimal step)
            : base(step)
        {
        }

        public AttrStep()
            : base()
        {
        }

        #endregion
    }
}
