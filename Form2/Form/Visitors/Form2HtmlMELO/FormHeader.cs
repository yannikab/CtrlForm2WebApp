using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Content.Items;
using Form2.Html.Content;
using Form2.Html.Content.Elements;
using Form2.Html.Content.Elements.Containers;

namespace Form2.Form.Visitors
{
    public partial class Form2HtmlMELOVisitor
    {
        public virtual void Visit(FormHeader formHeader, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = verbose ? new HtmlDiv(formHeader.Path) : new HtmlDiv();

            htmlDiv.Class.Add("formHeader");

            if (!string.IsNullOrWhiteSpace(formHeader.CssClass))
                htmlDiv.Class.AddRange(formHeader.CssClass.Split(' ').Where(s => s != string.Empty));

            htmlDiv.Class.AddRange(new string[] { "card-header", "mb-3" });

            if (!string.IsNullOrWhiteSpace(formHeader.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formHeader.Path));

            htmlDiv.Hidden.Value = formHeader.IsHidden;

            htmlContainer.Add(htmlDiv);

            htmlDiv.Add(new HtmlText(formHeader.Content));
        }
    }
}
