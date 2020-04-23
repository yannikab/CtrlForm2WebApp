using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
{
    public class AttrPlaceHolder : HtmlAttribute<string>
    {
        #region Properties

        public override string Name
        {
            get { return "placeholder"; }
        }

        #endregion


        #region Constructors

        public AttrPlaceHolder(string value)
            : base(value)
        {
        }

        public AttrPlaceHolder()
            : this(null)
        {
        }

        #endregion
    }
}
