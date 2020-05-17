using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Boolean
{
    public class AttrSelected : BooleanAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "selected"; }
        }

        #endregion


        #region Constructors

        public AttrSelected(bool value)
            : base(value)
        {
        }

        public AttrSelected()
            : base()
        {
        }

        #endregion
    }
}
