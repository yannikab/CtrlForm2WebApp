using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Integer
{
    public class AttrCols : IntegerAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "cols"; }
        }

        #endregion


        #region Constructors 

        public AttrCols(int cols)
            : base(cols)
        {
        }

        #endregion
    }
}
