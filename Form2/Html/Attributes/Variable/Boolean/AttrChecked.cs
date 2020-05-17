using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Boolean
{
    public class AttrChecked : BooleanAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "checked"; }
        }

        #endregion


        #region Constructors

        public AttrChecked(bool value)
            : base(value)
        {
        }

        public AttrChecked()
            : base()
        {
        }

        #endregion
    }
}
