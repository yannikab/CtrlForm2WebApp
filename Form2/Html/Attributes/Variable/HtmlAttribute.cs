using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Interfaces;

namespace Form2.Html.Attributes.Variable
{
    [SuppressMessage("Style", "IDE0034:Simplify 'default' expression", Justification = "<Pending>")]

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
            : this(default(T))
        {
        }

        #endregion
    }
}
