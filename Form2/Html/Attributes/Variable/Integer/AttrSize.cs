using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Integer
{
    public class AttrSize : IntegerAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "size"; }
        }

        #endregion


        #region Constructors 

        public AttrSize(int size)
            : base(size)
        {
        }

        public AttrSize()
            : base()
        {
        }

        #endregion
    }
}
