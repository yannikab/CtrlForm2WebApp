using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Interfaces;

namespace Form2.Html.Attributes.ReadOnly
{
    [SuppressMessage("Style", "IDE0034:Simplify 'default' expression", Justification = "<Pending>")]

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
            : this(default(T))
        {
        }

        #endregion
    }
}
