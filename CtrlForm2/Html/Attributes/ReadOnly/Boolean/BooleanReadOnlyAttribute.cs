using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Html.Attributes.ReadOnly.Boolean
{
    public abstract class BooleanReadOnlyAttribute : HtmlReadOnlyAttribute<bool?>
    {
        #region Properties

        public bool IsTrue
        {
            get { return IsSet && Value.Value; }
        }

        #endregion


        #region Constructors

        public BooleanReadOnlyAttribute(bool value)
            : base(value)
        {
        }

        public BooleanReadOnlyAttribute()
            : base()
        {
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return IsTrue ? string.Format(@" {0}", Name) : "";
        }

        #endregion
    }
}
