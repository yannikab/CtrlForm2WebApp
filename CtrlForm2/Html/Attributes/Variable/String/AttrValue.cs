using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Html.Attributes.Variable.String
{
    public class AttrValue : StringAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "value"; }
        }

        #endregion


        #region Constructors

        public AttrValue(string value)
            : base(value)
        {
        }

        public AttrValue()
            : this(null)
        {
        }

        #endregion
    }
}
