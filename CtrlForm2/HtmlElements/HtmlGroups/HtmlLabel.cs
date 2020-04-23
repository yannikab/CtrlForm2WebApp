using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.HtmlElements.HtmlAttributes;

namespace UserControls.CtrlForm2.HtmlElements.HtmlGroups
{
    public class HtmlLabel : HtmlGroup
    {
        #region Fields

        private readonly AttrFor @for;

        #endregion


        #region Properties

        public override string Tag
        {
            get { return "label"; }
        }

        protected override string IdPrefix
        {
            get { return "lbl"; }
        }

        public AttrFor For
        {
            get { return @for; }
        }

        #endregion


        #region Constructors

        public HtmlLabel(string baseId)
            : base(baseId)
        {
            @for = new AttrFor();
        }

        #endregion
    }
}
