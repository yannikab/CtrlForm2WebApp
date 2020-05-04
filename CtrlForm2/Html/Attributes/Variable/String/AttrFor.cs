using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Html.Attributes.Variable.String
{
    public class AttrFor : StringAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "for"; }
        }

        #endregion


        #region Constructors

        public AttrFor(string value)
            : base(value)
        {
        }

        public AttrFor()
            : this(null)
        {
        }

        #endregion
    }
}