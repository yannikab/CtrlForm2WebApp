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
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formLabel.BaseId : "");
            htmlDiv.Class.Add("form-label");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formLabel.FormId));

            htmlDiv.Hidden.Value = formLabel.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? formLabel.BaseId : "");
            htmlDiv.Add(htmlLabel);

            htmlLabel.Add(new HtmlText(formLabel.Content));
        }

        public virtual void Visit(FormTitle formTitle, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formTitle.BaseId : "");
            htmlDiv.Class.Add("form-title");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formTitle.FormId));

            htmlDiv.Hidden.Value = formTitle.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? formTitle.BaseId : "");
            htmlDiv.Add(htmlLabel);

            htmlLabel.Add(new HtmlText(formTitle.Content));
        }
    }
}
