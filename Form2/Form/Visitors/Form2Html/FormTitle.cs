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
    public partial class Form2HtmlVisitor
    {
        public virtual void Visit(FormTitle formTitle, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = verbose ? new HtmlDiv(formTitle.Path) : new HtmlDiv();

            htmlDiv.Class.Add("formTitle");

            if (!string.IsNullOrWhiteSpace(formTitle.CssClass))
                htmlDiv.Class.AddRange(formTitle.CssClass.Split(' ').Where(s => s != string.Empty));

            if (!string.IsNullOrWhiteSpace(formTitle.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formTitle.Path));

            htmlDiv.Hidden.Value = formTitle.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? formTitle.Path : string.Empty);
            htmlDiv.Add(htmlLabel);

            htmlLabel.Add(new HtmlText(formTitle.Content));
        }
    }
}
