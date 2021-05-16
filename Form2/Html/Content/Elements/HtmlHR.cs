using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Attributes.ReadOnly.String;
using Form2.Html.Attributes.Variable.Boolean;
using Form2.Html.Attributes.Variable.String;
using Form2.Html.Events;

namespace Form2.Html.Content.Elements.Containers
{
    public class HtmlHR : HtmlElement
    {
        public override string Tag
        {
            get { return "hr"; }
        }

        public HtmlHR()
            : base(string.Empty)
        {
        }
    }
}
