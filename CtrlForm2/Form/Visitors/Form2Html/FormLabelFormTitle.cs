using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Content.Items;
using CtrlForm2.Html.Content;
using CtrlForm2.Html.Content.Elements;
using CtrlForm2.Html.Content.Elements.Containers;

namespace CtrlForm2.Form.Visitors
{
    public partial class Form2HtmlVisitor
    {
        public virtual void Visit(FormLabel formLabel, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formLabel.BaseId);
            htmlDiv.Hidden.Value = formLabel.IsHidden;
            htmlDiv.Class.Add("form-label");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formLabel.FormId));

            htmlContainer.Add(htmlDiv);

            HtmlLabel htmlLabel = new HtmlLabel(formLabel.BaseId);
            htmlDiv.Add(htmlLabel);

            htmlLabel.Add(new HtmlText(formLabel.Label));
        }

        public virtual void Visit(FormTitle formTitle, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formTitle.BaseId);
            htmlDiv.Class.Add("form-title");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formTitle.FormId));
            htmlDiv.Hidden.Value = formTitle.IsHidden;
            htmlContainer.Add(htmlDiv);

            HtmlLabel htmlLabel = new HtmlLabel(formTitle.BaseId);
            htmlDiv.Add(htmlLabel);

            htmlLabel.Add(new HtmlText(formTitle.Label));
        }
    }
}
