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
        public virtual void Visit(FormLabel formLabel, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = verbose ? new HtmlDiv(formLabel.Path) : new HtmlDiv();

            htmlDiv.Class.Add("formLabel");

            if (!string.IsNullOrWhiteSpace(formLabel.CssClass))
                htmlDiv.Class.AddRange(formLabel.CssClass.Split(' ').Where(s => s != string.Empty));

            if (!string.IsNullOrWhiteSpace(formLabel.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formLabel.Path));

            htmlDiv.Hidden.Value = formLabel.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? formLabel.Path : "");
            htmlDiv.Add(htmlLabel);

            htmlLabel.Add(new HtmlText(formLabel.Content));
        }
    }
}
