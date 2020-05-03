using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.FormElements;
using UserControls.CtrlForm2.HtmlElements.HtmlGroups;

namespace UserControls.CtrlForm2.Visitors
{
    public partial class Form2HtmlVisitor
    {
        public virtual void Visit(FormGroup formGroup, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formGroup.BaseId);
            htmlDiv.Class.Add("form-grouping");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formGroup.FormId));
            htmlDiv.Hidden.Value = formGroup.IsHidden;

            if (htmlContainer == null)
                Html = htmlDiv;
            else
                htmlContainer.Add(htmlDiv);

            foreach (var formItem in formGroup.Items)
                Visit(formItem, htmlDiv);
        }
    }
}
