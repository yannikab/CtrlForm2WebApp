using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Integer
{
    public class AttrMax : DoubleAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "max"; }
        }

        #endregion


        #region Constructors 

        public AttrMax(double max)
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
