using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Html.Attributes.Variable.Boolean
{
    public class AttrReadOnly : BooleanAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "readonly"; }
        }

        #endregion


        #region Constructors

        public AttrReadOnly(bool value)
            : base(value)
        {
        }

        public AttrReadOnly()
            : base()
        {
        }

        #endregion
    }
}
