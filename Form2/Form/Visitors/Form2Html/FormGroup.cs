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
        public virtual void Visit(FormGroup formGroup, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = verbose ? new HtmlDiv(formGroup.Path) : new HtmlDiv();
            htmlDiv.Class.Add("formGroup");

            if (formGroup.Container == null)
            {
                if (!string.IsNullOrWhiteSpace(formGroup.Name))
                    htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formGroup.Name));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(formGroup.Path))
                    htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formGroup.Path));
            }

            htmlDiv.Hidden.Value = formGroup.IsHidden;

            if (htmlContainer == null)
                html = htmlDiv;
            else
                htmlContainer.Add(htmlDiv);

            foreach (var formItem in formGroup.Contents)
                Visit(formItem, htmlDiv);
        }
    }
}
