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
            HtmlDiv htmlDiv = new HtmlDiv(formTextBox.BaseId);
            htmlDiv.Class.Add("form-textbox");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formTextBox.FormId));
            htmlDiv.Class.Add("form-field");
            
            if (!validate)
            {
                htmlDiv.Class.Add(formTextBox.IsRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (!string.IsNullOrEmpty(formTextBox.Value))
                    htmlDiv.Class.Add(formTextBox.IsValid ? "form-valid" : "form-invalid");
                else
                    htmlDiv.Class.Add(formTextBox.IsRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formTextBox.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlTextBox htmlTextBox = new HtmlTextBox(formTextBox.BaseId);
            htmlTextBox.Disabled.Value = formTextBox.IsDisabled;
            htmlTextBox.ReadOnly.Value = formTextBox.IsReadOnly;
            htmlTextBox.Value.Value = formTextBox.Value;
            htmlTextBox.PlaceHolder.Value = !string.IsNullOrEmpty(formTextBox.PlaceHolder) ? formTextBox.PlaceHolder : null;

            HtmlLabel htmlLabel = new HtmlLabel(formTextBox.BaseId);
            htmlLabel.For.Value = htmlTextBox.Id.Value;

            switch (formTextBox.ElementOrder)
            {
                case ElementOrder.LabelMarkInput:

                    htmlLabel.Add(new HtmlText(formTextBox.Label));

                    if (formTextBox.IsRequired && !string.IsNullOrWhiteSpace(formTextBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formTextBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextBox);

                    break;

                case ElementOrder.MarkLabelInput:

                    if (formTextBox.IsRequired && !string.IsNullOrWhiteSpace(formTextBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formTextBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formTextBox.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextBox);

                    break;

                case ElementOrder.InputLabelMark:

                    htmlLabel.Add(new HtmlText(formTextBox.Label));

                    if (formTextBox.IsRequired && !string.IsNullOrWhiteSpace(formTextBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formTextBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlTextBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.InputMarkLabel:

                    if (formTextBox.IsRequired && !string.IsNullOrWhiteSpace(formTextBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formTextBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formTextBox.Label));

                    htmlDiv.Add(htmlTextBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.LabelInputMark:

                    htmlLabel.Add(new HtmlText(formTextBox.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextBox);

                    if (formTextBox.IsRequired && !string.IsNullOrWhiteSpace(formTextBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formTextBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    break;

                case ElementOrder.MarkInputLabel:

                    if (formTextBox.IsRequired && !string.IsNullOrWhiteSpace(formTextBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formTextBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formTextBox.Label));

                    htmlDiv.Add(htmlTextBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                default:
                case ElementOrder.NotSet:

                    break;
            }

            if (!validate)
                return;

            if (sessionState != null)
            {
                if (sessionState[formTextBox.SessionKey] == null)
                    return;

                formTextBox.Content = (string)sessionState[formTextBox.SessionKey];
            }

            string message = null;

            if (formTextBox.IsRequired && !formTextBox.HasValue)
                message = formTextBox.RequiredMessage;
            else if (!formTextBox.IsValid)
                message = formTextBox.ValidationMessage;

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formTextBox.BaseId, "Message"));
            htmlLabelMessage.Class.Add("form-validation-message");
            htmlLabelMessage.For.Value = htmlTextBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }

        public virtual void Visit(FormTextArea formTextArea, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formTextArea.BaseId);
            htmlDiv.Class.Add("form-textarea");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formTextArea.FormId));
            htmlDiv.Class.Add("form-field");
            
            if (!validate)
            {
                htmlDiv.Class.Add(formTextArea.IsRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (!string.IsNullOrEmpty(formTextArea.Value))
                    htmlDiv.Class.Add(formTextArea.IsValid ? "form-valid" : "form-invalid");
                else
                    htmlDiv.Class.Add(formTextArea.IsRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formTextArea.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlTextArea htmlTextArea = new HtmlTextArea(formTextArea.BaseId, formTextArea.Rows, formTextArea.Columns);
            htmlTextArea.Disabled.Value = formTextArea.IsDisabled;
            htmlTextArea.ReadOnly.Value = formTextArea.IsReadOnly;
            htmlTextArea.Value.Value = formTextArea.Value;
            htmlTextArea.PlaceHolder.Value = !string.IsNullOrEmpty(formTextArea.PlaceHolder) ? formTextArea.PlaceHolder : null;

            HtmlLabel htmlLabel = new HtmlLabel(formTextArea.BaseId);
            htmlLabel.For.Value = htmlTextArea.Id.Value;

            switch (formTextArea.ElementOrder)
            {
                case ElementOrder.LabelMarkInput:

                    htmlLabel.Add(new HtmlText(formTextArea.Label));

                    if (formTextArea.IsRequired && !string.IsNullOrWhiteSpace(formTextArea.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan(formTextArea.BaseId);
                        htmlSpan.Add(new HtmlText(formTextArea.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextArea);

                    break;

                case ElementOrder.MarkLabelInput:

                    if (formTextArea.IsRequired && !string.IsNullOrWhiteSpace(formTextArea.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan(formTextArea.BaseId);
                        htmlSpan.Add(new HtmlText(formTextArea.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formTextArea.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextArea);

                    break;

                case ElementOrder.InputLabelMark:

                    htmlLabel.Add(new HtmlText(formTextArea.Label));

                    if (formTextArea.IsRequired && !string.IsNullOrWhiteSpace(formTextArea.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan(formTextArea.BaseId);
                        htmlSpan.Add(new HtmlText(formTextArea.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlTextArea);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.InputMarkLabel:

                    if (formTextArea.IsRequired && !string.IsNullOrWhiteSpace(formTextArea.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan(formTextArea.BaseId);
                        htmlSpan.Add(new HtmlText(formTextArea.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formTextArea.Label));

                    htmlDiv.Add(htmlTextArea);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.LabelInputMark:

                    htmlLabel.Add(new HtmlText(formTextArea.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextArea);

                    if (formTextArea.IsRequired && !string.IsNullOrWhiteSpace(formTextArea.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan(formTextArea.BaseId);
                        htmlSpan.Add(new HtmlText(formTextArea.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    break;

                case ElementOrder.MarkInputLabel:

                    if (formTextArea.IsRequired && !string.IsNullOrWhiteSpace(formTextArea.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan(formTextArea.BaseId);
                        htmlSpan.Add(new HtmlText(formTextArea.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formTextArea.Label));

                    htmlDiv.Add(htmlTextArea);
                    htmlDiv.Add(htmlLabel);

                    break;

                default:
                case ElementOrder.NotSet:

                    break;
            }

            if (!validate)
                return;

            if (sessionState != null)
            {
                if (sessionState[formTextArea.SessionKey] == null)
                    return;

                formTextArea.Content = (string)sessionState[formTextArea.SessionKey];
            }

            string message = null;

            if (formTextArea.IsRequired && !formTextArea.HasValue)
                message = formTextArea.RequiredMessage;
            else if (!formTextArea.IsValid)
                message = formTextArea.ValidationMessage;

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formTextArea.BaseId, "Message"));
            htmlLabelMessage.Class.Add("form-validation-message");
            htmlLabelMessage.For.Value = htmlTextArea.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
