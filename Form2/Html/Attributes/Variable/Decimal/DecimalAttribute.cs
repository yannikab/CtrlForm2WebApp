using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Decimal
{
    public abstract class DecimalAttribute : HtmlAttribute<decimal?>
    {
        #region Properties

        public override bool IsSet
        {
            get { return Value.HasValue; }
        }

        #endregion


        #region Constructors

        public DecimalAttribute(decimal value)
            : base(value)
        {
        }

        public DecimalAttribute()
            : base(null)
        {
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return IsSet ? string.Format(CultureInfo.InvariantCulture, @" {0}=""{1}""", Name, Value) : "";
        }

        #endregion
    }
}
