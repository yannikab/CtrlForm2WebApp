﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Content.Items;
using Form2.Form.Enums;
using Form2.Html.Content;
using Form2.Html.Content.Elements;
using Form2.Html.Content.Elements.Containers;

namespace Form2.Form.Visitors
{
    public partial class Form2HtmlMELOVisitor
    {
        public virtual void Visit(FormSubmit formSubmit, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = verbose ? new HtmlDiv(formSubmit.Path) : new HtmlDiv();

            htmlDiv.Class.Add("formButton");

            if (!string.IsNullOrWhiteSpace(formSubmit.CssClass))
                htmlDiv.Class.AddRange(formSubmit.CssClass.Split(' ').Where(s => s != string.Empty));

            if (!string.IsNullOrWhiteSpace(formSubmit.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formSubmit.Path));

            htmlDiv.Hidden.Value = formSubmit.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlSubmit htmlSubmit = new HtmlSubmit(formSubmit.Path, verbose, formSubmit.Parameter);
            htmlSubmit.Class.AddRange(new string[] { "btnFix", "btn", "mt-2" });

            switch (formSubmit.Type)
            {
                case ButtonType.Primary:
                case ButtonType.Secondary:
                case ButtonType.Success:
                case ButtonType.Danger:
                    htmlSubmit.Class.Add(string.Format("btn-{0}", formSubmit.Type.ToString().ToLower()));
                    break;
                default:
                case ButtonType.NotSet:
                    break;
            }

            htmlSubmit.Disabled.Value = formSubmit.IsDisabled;
            htmlDiv.Add(htmlSubmit);

            htmlSubmit.Add(new HtmlText(formSubmit.Content));
        }
    }
}
