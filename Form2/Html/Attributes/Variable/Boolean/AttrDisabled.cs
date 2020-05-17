using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Boolean
{
    public class AttrDisabled : BooleanAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "disabled"; }
        }

        #endregion


        #region Constructors

        public AttrDisabled(bool value)
            : base(value)
        {
        }

        public AttrDisabled()
            : base()
        {
        }

        #endregion
    }
}
