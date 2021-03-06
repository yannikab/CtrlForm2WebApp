﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Integer
{
    public abstract class IntegerAttribute : HtmlAttribute<long?>
    {
        #region Properties

        public override bool IsSet
        {
            get { return Value.HasValue; }
        }

        #endregion


        #region Constructors

        public IntegerAttribute(long value)
            : base(value)
        {
        }

        public IntegerAttribute()
            : base(null)
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
