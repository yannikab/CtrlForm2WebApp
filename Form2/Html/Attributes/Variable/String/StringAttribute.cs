using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.String
{
    public abstract class StringAttribute : HtmlAttribute<string>
    {
        #region Constructors

        public StringAttribute(string value)
            : base(value)
        {
        }

        public StringAttribute()
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
