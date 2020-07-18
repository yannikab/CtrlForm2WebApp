using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Integer
{
    public class AttrMin : DoubleAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "min"; }
        }

        #endregion


        #region Constructors 

        public AttrMin(double min)
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
