using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Content.Elements.Containers
{
    public class HtmlFieldset : HtmlContainer
    {
        #region Properties

        public override string Tag
        {
            get { return "fieldset"; }
        }

        protected override string Prefix
        {
            get { return "fld"; }
        }

        #endregion


        #region Constructors

        public HtmlFieldset(string name)
            : base(name)
        {
        }

        public HtmlFieldset()
            : this(string.Empty)
        {
        }

        #endregion
    }
}
