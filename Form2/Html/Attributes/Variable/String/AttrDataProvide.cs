using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.String
{
    public class AttrDataProvide : StringAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "data-provide"; }
        }

        #endregion


        #region Constructors

        public AttrDataProvide(string value)
            : base(value)
        {
        }

        public AttrDataProvide()
            : this(null)
        {
        }

        #endregion
    }
}
