using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.ReadOnly.String
{
    public class AttrName : StringReadOnlyAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "name"; }
        }

        #endregion


        #region Constructors

        public AttrName(string value)
            : base(value)
        {
        }

        public AttrName()
            : base()
        {
        }

        #endregion
    }
}
