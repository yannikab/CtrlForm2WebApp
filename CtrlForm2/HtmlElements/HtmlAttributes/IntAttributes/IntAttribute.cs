using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
{
    public abstract class IntAttribute : HtmlAttribute<int?>
    {
        #region Properties

        public override bool IsSet
        {
            get { return Value.HasValue; }
        }

        #endregion


        #region Constructors

        public IntAttribute(int? value)
            : base(value)
        {
        }

        public IntAttribute()
            : this(null)
        {
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return IsSet ? string.Format(@" {0}=""{1}""", Name, Value) : "";
        }

        #endregion
    }
}
