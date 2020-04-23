using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.HtmlElements.HtmlAttributes
{
    public class AttrFor : HtmlAttribute<string>
    {
        #region Properties

        public override string Name
        {
            get { return "for"; }
        }

        #endregion


        #region Constructors

        public AttrFor(string value)
            : base(value)
        {
        }

        public AttrFor()
            : this(null)
        {
        }

        #endregion
    }
}