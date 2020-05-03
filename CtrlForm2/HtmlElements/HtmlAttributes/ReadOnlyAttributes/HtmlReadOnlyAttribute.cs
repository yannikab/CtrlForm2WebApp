using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.Interfaces;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
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
            get { return value != null; }
        }

        public virtual T Value
        {
            get { return value; }
        }

        #endregion


        #region Constructors

        public HtmlReadOnlyAttribute(T value)
        {
            this.value = value;
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
