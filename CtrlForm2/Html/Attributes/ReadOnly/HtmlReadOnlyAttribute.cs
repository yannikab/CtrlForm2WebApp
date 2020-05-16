﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Html.Interfaces;

namespace CtrlForm2.Html.Attributes.ReadOnly
{
    public abstract class HtmlReadOnlyAttribute<T> : IHtmlAttribute
    {
        #region Fields

        private readonly T value;

        #endregion


        #region Properties

        public abstract string Name { get; }

        public virtual bool IsSet
        {
            get { return !Equals(value, default(T)); }
        }

        public T Value
        {
            get { return value; }
        }

        #endregion


        #region Constructors

        public HtmlReadOnlyAttribute(T value)
        {
            this.value = value;
        }

        public HtmlReadOnlyAttribute()
            : this(default)
        {
        }

        #endregion
    }
}