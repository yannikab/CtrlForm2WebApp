using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.MultiValue
{
    public class AttrClass : HtmlMultiValueAttribute<string>
    {
        #region Properties

        public override string Name
        {
            get { return "class"; }
        }

        #endregion


        #region Constructors

        public AttrClass(IEnumerable<string> values)
            : base(values)
        {
        }

        public AttrClass()
            : base(Enumerable.Empty<string>())
        {
        }

        #endregion
    }
}
