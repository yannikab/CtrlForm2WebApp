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
        public virtual void Visit(FormButton formButton, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = verbose ? new HtmlDiv(formButton.Path) : new HtmlDiv();

            htmlDiv.Class.Add("formButton");

            if (!string.IsNullOrWhiteSpace(formButton.CssClass))
                htmlDiv.Class.AddRange(formButton.CssClass.Split(' ').Where(s => s != string.Empty));

            if (!string.IsNullOrWhiteSpace(formButton.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formButton.Path));

            htmlDiv.Hidden.Value = formButton.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlButton htmlButton = new HtmlButton(formButton.Path);
            htmlButton.Disabled.Value = formButton.IsDisabled;
            htmlDiv.Add(htmlButton);

            htmlButton.Add(new HtmlText(formButton.Content));
        }
    }
}
