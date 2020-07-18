using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Integer
{
    public class AttrRows : IntegerAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "rows"; }
        }

        #endregion


        #region Constructors 

        public AttrRows(long rows)
            : base(rows)
        {
        }

        public AttrRows()
            : base()
        {
        }

        #endregion
    }
}
