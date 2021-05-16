using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Content.Items;
using Form2.Html.Content.Elements;
using Form2.Html.Content.Elements.Containers;

namespace Form2.Form.Visitors
{
    public partial class Form2HtmlMELOVisitor
    {
        public virtual void Visit(FormFooter formFooter, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = verbose ? new HtmlDiv(formFooter.Path) : new HtmlDiv();

            htmlDiv.Class.Add("formFooter");

            if (!string.IsNullOrWhiteSpace(formFooter.CssClass))
                htmlDiv.Class.AddRange(formFooter.CssClass.Split(' ').Where(s => s != string.Empty));

            htmlDiv.Class.Add("card-footer");

            if (!string.IsNullOrWhiteSpace(formFooter.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formFooter.Path));

            htmlDiv.Hidden.Value = formFooter.IsHidden;

            htmlContainer.Add(htmlDiv);
        }
    }
}
