﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Html.Interfaces;

namespace CtrlForm2.Html.Attributes.Variable
{
    public abstract class HtmlAttribute<T> : IHtmlAttribute
    {
        #region Fields

        private T value;

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
            set { this.value = value; }
        }

        #endregion


        #region Constructors

        public HtmlAttribute(T value)
        {
            this.value = value;
        }

        public HtmlAttribute()
            : this(default)
        {
        }

        #endregion
    }
}