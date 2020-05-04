using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Html.Attributes.ReadOnly.Boolean
{
    public class AttrMultiple : BooleanReadOnlyAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "multiple"; }
        }

        #endregion


        #region Constructors

        public AttrMultiple(bool value)
            : base(value)
        {
        }

        public AttrMultiple()
            : base()
        {
        }

        #endregion
    }
}
