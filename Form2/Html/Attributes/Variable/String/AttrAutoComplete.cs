using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.String
{
    public class AttrAutoComplete : StringAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "autocomplete"; }
        }

        #endregion


        #region Constructors

        public AttrAutoComplete(string value)
            : base(value)
        {
        }

        public AttrAutoComplete()
            : this(null)
        {
        }

        #endregion
    }
}
