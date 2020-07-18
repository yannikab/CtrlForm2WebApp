﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Integer
{
    public abstract class DoubleAttribute : HtmlAttribute<double?>
    {
        #region Properties

        public override bool IsSet
        {
            get { return Value.HasValue; }
        }

        #endregion


        #region Constructors

        public DoubleAttribute(double value)
            : base(value)
        {
        }

        public DoubleAttribute()
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
