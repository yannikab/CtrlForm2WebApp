using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
{
    public abstract class BooleanAttribute : HtmlAttribute<bool>
    {
        #region Properties

        public override bool IsSet
        {
            get { return Value; }
        }

        #endregion


        #region Constructors

        public BooleanAttribute(bool value)
            : base(value)
        {
        }

        public BooleanAttribute()
            : this(false)
        {
        }

        #endregion
    }
}
