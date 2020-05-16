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
        public virtual void Visit(FormSubmit formSubmit, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formSubmit.BaseId);
            htmlDiv.Class.Add("form-button");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formSubmit.FormId));
            htmlDiv.Hidden.Value = formSubmit.IsHidden;
            htmlContainer.Add(htmlDiv);

            HtmlSubmit htmlSubmit = new HtmlSubmit(formSubmit.BaseId);
            htmlSubmit.Disabled.Value = formSubmit.IsDisabled;
            htmlDiv.Add(htmlSubmit);

            htmlSubmit.Add(new HtmlText(formSubmit.Content));
        }
    }
}
