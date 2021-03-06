﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Content.Items.Input;
using Form2.Form.Enums;
using Form2.Html.Content;
using Form2.Html.Content.Elements;
using Form2.Html.Content.Elements.Containers;
using Form2.Html.Content.Elements.Input;

namespace Form2.Form.Visitors
{
    public partial class Form2HtmlVisitor
    {
        public virtual void Visit(FormTextArea formTextArea, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = verbose ? new HtmlDiv(formTextArea.Path) : new HtmlDiv();

            htmlDiv.Class.Add("formTextArea");

            if (!string.IsNullOrWhiteSpace(formTextArea.CssClass))
                htmlDiv.Class.AddRange(formTextArea.CssClass.Split(' ').Where(s => s != string.Empty));

            if (!string.IsNullOrWhiteSpace(formTextArea.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formTextArea.Path));

            htmlDiv.Class.Add("formField");

            if (initialize)
            {
                htmlDiv.Class.Add(formTextArea.IsRequired ? "formRequired" : "formOptional");
            }
            else
            {
                if (formTextArea.HasValue)
                    htmlDiv.Class.Add(formTextArea.IsValid ? "formValid" : "formInvalid");
                else
                    htmlDiv.Class.Add(formTextArea.IsRequired ? "formNotEntered" : "formOptional");
            }

            htmlDiv.Hidden.Value = formTextArea.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlTextArea htmlTextArea = new HtmlTextArea(formTextArea.Path);
            htmlTextArea.Disabled.Value = formTextArea.IsDisabled;
            htmlTextArea.ReadOnly.Value = formTextArea.IsReadOnly;
            htmlTextArea.Value.Value = formTextArea.Value;

            string placeholder = null;

            if (!string.IsNullOrWhiteSpace(formTextArea.Placeholder))
            {
                if (formTextArea.IsRequired && formTextArea.IsRequiredInPlaceholder && !string.IsNullOrWhiteSpace(formTextArea.RequiredMark))
                    placeholder = string.Format("{0} {1}", formTextArea.Placeholder, formTextArea.RequiredMark);
                else if (!formTextArea.IsRequired && formTextArea.IsOptionalInPlaceholder && !string.IsNullOrWhiteSpace(formTextArea.OptionalMark))
                    placeholder = string.Format("{0} {1}", formTextArea.Placeholder, formTextArea.OptionalMark);
                else
                    placeholder = formTextArea.Placeholder;
            }

            htmlTextArea.Placeholder.Value = placeholder;

            htmlTextArea.Rows.Value = formTextArea.Rows;
            htmlTextArea.Cols.Value = formTextArea.Columns;

            if (!initialize && firstInvalidId == null)
                if (formTextArea.IsRequired && !formTextArea.HasValue || !formTextArea.IsValid)
                    firstInvalidId = htmlTextArea.Id.Value;

            switch (formTextArea.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    AddLabelMark(formTextArea, htmlTextArea, htmlDiv);
                    htmlDiv.Add(htmlTextArea);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMarkLabel(formTextArea, htmlTextArea, htmlDiv);
                    htmlDiv.Add(htmlTextArea);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlTextArea);
                    AddLabelMark(formTextArea, htmlTextArea, htmlDiv);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlTextArea);
                    AddMarkLabel(formTextArea, htmlTextArea, htmlDiv);

                    break;

                case OrderElements.LabelInputMark:

                    AddLabel(formTextArea, htmlTextArea, htmlDiv);
                    htmlDiv.Add(htmlTextArea);
                    AddMark(formTextArea, htmlTextArea, htmlDiv);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formTextArea, htmlTextArea, htmlDiv);
                    htmlDiv.Add(htmlTextArea);
                    AddLabel(formTextArea, htmlTextArea, htmlDiv);

                    break;

                default:
                case OrderElements.NotSet:

                    break;
            }

            if (initialize)
                return;

            string message = null;

            if (formTextArea.UseLastMessage)
            {
                if (!string.IsNullOrEmpty(formTextArea.LastMessage))
                    message = formTextArea.LastMessage;
            }
            else if (formTextArea.IsRequired && !formTextArea.HasValue)
            {
                message = formTextArea.RequiredMessage;
            }
            else if (!formTextArea.IsValid)
            {
                message = formTextArea.ValidationMessage;
            }

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formTextArea.Path, "Message") : "");
            htmlLabelMessage.Class.Add("formValidationMessage");
            htmlLabelMessage.For.Value = htmlTextArea.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
