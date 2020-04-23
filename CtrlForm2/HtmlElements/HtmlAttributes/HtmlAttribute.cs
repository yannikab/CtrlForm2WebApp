using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.Interfaces;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
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
            get { return value != null; }
        }

        public virtual T Value
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

        #endregion


        #region Object

        public override string ToString()
        {
            return IsSet ? string.Format(@" {0}=""{1}""", Name, Value) : "";
        }

        #endregion
    }
}
