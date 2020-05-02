﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
{
    public abstract class BooleanAttribute : HtmlAttribute<bool?>
    {
        #region Properties

        public override bool IsSet
        {
            get { return Value.HasValue; }
        }

        public bool IsTrue
        {
            get { return IsSet && Value.Value; }
        }

        #endregion


        #region Constructors

        public BooleanAttribute(bool? value)
            : base(value)
        {
        }

        public BooleanAttribute()
            : this(null)
        {
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return IsTrue ? string.Format(" {0}", Name) : "";
        }

        #endregion
    }
}
