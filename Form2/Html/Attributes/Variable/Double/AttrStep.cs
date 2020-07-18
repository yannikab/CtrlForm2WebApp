using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Integer
{
    public class AttrStep : DoubleAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "step"; }
        }

        #endregion


        #region Constructors 

        public AttrStep(double min)
            : base(min)
        {
        }

        public AttrStep()
            : base()
        {
        }

        #endregion
    }
}
