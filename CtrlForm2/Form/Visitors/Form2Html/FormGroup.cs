﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Content;
using CtrlForm2.Html.Content.Elements;
using CtrlForm2.Html.Content.Elements.Containers;

namespace CtrlForm2.Form.Visitors
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

            foreach (var formItem in formGroup.Contents)
                Visit(formItem, htmlDiv);
        }
    }
}
