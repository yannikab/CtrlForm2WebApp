using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Html.Attributes.ReadOnly.String
{
    public class AttrType : StringReadOnlyAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "type"; }
        }

        #endregion


        #region Constructors

        public AttrType(string value)
            : base(value)
        {
        }

        public AttrType()
            : base()
        {
        }

        #endregion
    }
}
