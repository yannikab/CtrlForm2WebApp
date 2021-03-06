﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.ReadOnly.String
{
    public abstract class StringReadOnlyAttribute : HtmlReadOnlyAttribute<string>
    {
        #region Constructors

        public StringReadOnlyAttribute(string value)
             : base(!string.IsNullOrEmpty(value) ? value : null)
        {
        }

        public StringReadOnlyAttribute()
            : base()
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
