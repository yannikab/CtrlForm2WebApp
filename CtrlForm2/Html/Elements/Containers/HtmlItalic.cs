using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Html.Elements.Containers
{
    public class HtmlItalic : HtmlContainer
    {
        #region Properties

        public override string Tag
        {
            get { return "i"; }
        }

        protected override string IdPrefix
        {
            get { return "itl"; }
        }

        #endregion


        #region Constructors

        public HtmlItalic(string baseId)
            : base(baseId)
        {
        }

        public HtmlItalic()
            : this("")
        {
        }

        #endregion
    }
}
