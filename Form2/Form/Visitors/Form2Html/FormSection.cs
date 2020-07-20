using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Content;
using Form2.Html.Content.Elements;
using Form2.Html.Content.Elements.Containers;

namespace Form2.Form.Visitors
{
    public partial class Form2HtmlVisitor
    {
        public virtual void Visit(FormSection formSection, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formSection.BaseId : "");
            htmlDiv.Class.Add("form-section");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formSection.FormId));

            htmlDiv.Hidden.Value = formSection.IsHidden;

            if (htmlContainer == null)
                Html = htmlDiv;
            else
                htmlContainer.Add(htmlDiv);

            foreach (var formItem in formSection.Contents)
                Visit(formItem, htmlDiv);
        }
    }
}
