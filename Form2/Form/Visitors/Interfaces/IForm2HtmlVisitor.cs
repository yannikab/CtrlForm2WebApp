using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Content.Elements;

namespace Form2.Form.Visitors.Interfaces
{
    interface IForm2HtmlVisitor
    {
        HtmlContainer Html { get; }

        IReadOnlyList<HtmlContainer> Scripts { get; }
    }
}
