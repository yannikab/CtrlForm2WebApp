using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Attributes.Variable.String;

namespace Form2.Html.Content.Elements.Containers
{
    public class HtmlH3 : HtmlContainer
    {
        #region Properties

        public override string Tag
        {
            get { return "h3"; }
        }

        protected override string Prefix
        {
            get { return "h3"; }
        }

        #endregion


        #region Constructors

        public HtmlH3()
            : base(string.Empty)
        {
        }

        #endregion
    }
}
