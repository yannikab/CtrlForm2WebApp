using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Boolean
{
    public class AttrHidden : BooleanAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "hidden"; }
        }

        #endregion


        #region Constructors

        public AttrHidden(bool value)
            : base(value)
        {
        }

        public AttrHidden()
            : base()
        {
        }

        #endregion
    }
}
