using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.HtmlElements.HtmlAttributes;

namespace UserControls.CtrlForm2.HtmlElements.HtmlGroups
{
    public class HtmlSubmit : HtmlContainer
    {
        #region fields

        private readonly AttrType attrType;

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
            get { return attrType; }
        }

        #endregion


        #region Constructors

        public HtmlSubmit(string baseId)
            : base(baseId)
        {
            attributes.Add(attrType = new AttrType("submit"));
        }

        #endregion
    }
}
