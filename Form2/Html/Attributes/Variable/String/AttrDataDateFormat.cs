using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.String
{
    public class AttrDataDateFormat : StringAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "data-date-format"; }
        }

        #endregion


        #region Constructors

        public AttrDataDateFormat(string value)
            : base(value)
        {
        }

        public AttrDataDateFormat()
            : this(null)
        {
        }

        #endregion
    }
}
