using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.ReadOnly.String
{
    public class AttrId : StringReadOnlyAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "id"; }
        }

        #endregion


        #region Constructors

        public AttrId(string value)
            : base(value)
        {
        }

        public AttrId()
            : base()
        {
        }

        #endregion
    }
}
