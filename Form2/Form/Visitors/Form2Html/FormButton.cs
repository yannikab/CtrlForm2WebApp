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
            HtmlDiv htmlDiv = new HtmlDiv(formButton.BaseId);
            htmlDiv.Class.Add("form-item");
            htmlDiv.Class.Add("form-button");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formButton.FormId));
            htmlDiv.Hidden.Value = formButton.IsHidden;
            htmlContainer.Add(htmlDiv);

            HtmlSubmit htmlSubmit = new HtmlSubmit(formButton.BaseId);
            htmlSubmit.Disabled.Value = formButton.IsDisabled;
            htmlDiv.Add(htmlSubmit);

            htmlSubmit.Add(new HtmlText(formButton.Content));
        }
    }
}
