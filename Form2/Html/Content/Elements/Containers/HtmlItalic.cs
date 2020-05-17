using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Content.Elements.Containers
{
    public class HtmlItalic : HtmlContainer
    {
        #region Properties

        public override string Tag
        {
            get { return "i"; }
        }

        protected override string Prefix
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
