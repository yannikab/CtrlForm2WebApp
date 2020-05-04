using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Html.Attributes.Variable.Integer
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

        public AttrRows(int rows)
            : base(rows)
        {
        }

        #endregion
    }
}
