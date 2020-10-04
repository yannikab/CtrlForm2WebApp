using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Content.Elements.Containers
{
    public class HtmlScript : HtmlContainer
    {
        #region Properties

        public override string Tag
        {
            get { return "script"; }
        }

        #endregion


        #region Constructors

        public HtmlScript()
            : base("")
        {
        }

        #endregion
    }
}
