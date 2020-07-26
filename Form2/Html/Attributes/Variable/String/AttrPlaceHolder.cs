using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.String
{
    public class AttrPlaceholder : StringAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "placeholder"; }
        }

        #endregion


        #region Constructors

        public AttrPlaceholder(string value)
            : base(value)
        {
        }

        public AttrPlaceholder()
            : this(null)
        {
        }

        #endregion
    }
}
