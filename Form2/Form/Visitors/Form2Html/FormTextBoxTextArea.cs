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
        public virtual void Visit(FormTextBox formTextBox, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formTextBox.Name : "");
            htmlDiv.Class.Add("formTextBox");
            if (!string.IsNullOrWhiteSpace(formTextBox.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formTextBox.Path));
            htmlDiv.Class.Add("formField");

            if (initialize)
            {
                htmlDiv.Class.Add(formTextBox.IsRequired ? "formRequired" : "formOptional");
            }
            else
            {
                if (formTextBox.HasValue)
                    htmlDiv.Class.Add(formTextBox.IsValid ? "formValid" : "formInvalid");
                else
                    htmlDiv.Class.Add(formTextBox.IsRequired ? "formNotEntered" : "formOptional");
            }

            htmlDiv.Hidden.Value = formTextBox.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlTextBox htmlTextBox = new HtmlTextBox(formTextBox.Path);
            htmlTextBox.Disabled.Value = formTextBox.IsDisabled;
            htmlTextBox.ReadOnly.Value = formTextBox.IsReadOnly;
            htmlTextBox.Value.Value = formTextBox.Value;

            string placeholder = null;

            if (!string.IsNullOrWhiteSpace(formTextBox.Placeholder))
            {
                if (formTextBox.IsRequired && formTextBox.IsRequiredInPlaceholder && !string.IsNullOrWhiteSpace(formTextBox.RequiredMark))
                    placeholder = string.Format("{0} {1}", formTextBox.Placeholder, formTextBox.RequiredMark);
                else if (!formTextBox.IsRequired && formTextBox.IsOptionalInPlaceholder && !string.IsNullOrWhiteSpace(formTextBox.OptionalMark))
                    placeholder = string.Format("{0} {1}", formTextBox.Placeholder, formTextBox.OptionalMark);
                else
                    placeholder = formTextBox.Placeholder;
            }

            htmlTextBox.Placeholder.Value = placeholder;

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? formTextBox.Path : "");
            htmlLabel.For.Value = htmlTextBox.Id.Value;
            htmlLabel.Add(new HtmlText(formTextBox.Label));

            switch (formTextBox.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    htmlDiv.Add(htmlLabel);
                    AddMark(formTextBox, htmlDiv);
                    htmlDiv.Add(htmlTextBox);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMark(formTextBox, htmlDiv);
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextBox);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlTextBox);
                    htmlDiv.Add(htmlLabel);
                    AddMark(formTextBox, htmlDiv);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlTextBox);
                    AddMark(formTextBox, htmlDiv);
                    htmlDiv.Add(htmlLabel);

                    break;

                case OrderElements.LabelInputMark:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextBox);
                    AddMark(formTextBox, htmlDiv);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formTextBox, htmlDiv);
                    htmlDiv.Add(htmlTextBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                default:
                case OrderElements.NotSet:

                    break;
            }

            if (initialize)
                return;

            string message = null;

            if (formTextBox.UseLastMessage)
            {
                if (!string.IsNullOrEmpty(formTextBox.LastMessage))
                    message = formTextBox.LastMessage;
            }
            else if (formTextBox.IsRequired && !formTextBox.HasValue)
            {
                message = formTextBox.RequiredMessage;
            }
            else if (!formTextBox.IsValid)
            {
                message = formTextBox.ValidationMessage;
            }

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formTextBox.Name, "Message") : "");
            htmlLabelMessage.Class.Add("formValidationMessage");
            htmlLabelMessage.For.Value = htmlTextBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }

        public virtual void Visit(FormTextArea formTextArea, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formTextArea.Path : "");
            htmlDiv.Class.Add("formTextArea");
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

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? formTextArea.Path : "");
            htmlLabel.For.Value = htmlTextArea.Id.Value;
            htmlLabel.Add(new HtmlText(formTextArea.Label));

            switch (formTextArea.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    htmlDiv.Add(htmlLabel);
                    AddMark(formTextArea, htmlDiv);
                    htmlDiv.Add(htmlTextArea);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMark(formTextArea, htmlDiv);
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextArea);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlTextArea);
                    htmlDiv.Add(htmlLabel);
                    AddMark(formTextArea, htmlDiv);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlTextArea);
                    AddMark(formTextArea, htmlDiv);
                    htmlDiv.Add(htmlLabel);

                    break;

                case OrderElements.LabelInputMark:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextArea);
                    AddMark(formTextArea, htmlDiv);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formTextArea, htmlDiv);
                    htmlDiv.Add(htmlTextArea);
                    htmlDiv.Add(htmlLabel);

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
