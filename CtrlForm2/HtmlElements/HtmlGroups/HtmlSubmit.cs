using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.HtmlElements.HtmlAttributes;

namespace UserControls.CtrlForm2.HtmlElements.HtmlGroups
{
    public class HtmlSubmit : HtmlGroup
    {
        #region fields

        private readonly AttrType type;

        #endregion

        #region Properties

        public override string Tag
        {
            get { return "button"; }
        }

        protected override string IdPrefix
        {
            get { return "btn"; }
        }

        public AttrType Type
        {
            get { return type; }
        }

        #endregion


        #region Constructors

        public HtmlSubmit(string baseId)
            : base(baseId)
        {
            type = new AttrType("submit");
        }

        #endregion
    }
}
