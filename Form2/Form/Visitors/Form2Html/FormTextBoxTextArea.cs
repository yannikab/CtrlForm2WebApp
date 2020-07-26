using System;
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
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formTextBox.BaseId : "");
            htmlDiv.Class.Add("form-textbox");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formTextBox.FormId));
            htmlDiv.Class.Add("form-field");

            if (initialize)
            {
                htmlDiv.Class.Add(formTextBox.IsRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (formTextBox.HasValue)
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

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? formTextBox.BaseId : "");
            htmlLabel.For.Value = htmlTextBox.Id.Value;
            htmlLabel.Add(new HtmlText(formTextBox.Label));

            switch (formTextBox.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(Mark(formTextBox));
                    htmlDiv.Add(htmlTextBox);

                    break;

                case OrderElements.MarkLabelInput:

                    htmlDiv.Add(Mark(formTextBox));
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextBox);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlTextBox);
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(Mark(formTextBox));

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlTextBox);
                    htmlDiv.Add(Mark(formTextBox));
                    htmlDiv.Add(htmlLabel);

                    break;

                case OrderElements.LabelInputMark:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextBox);
                    htmlDiv.Add(Mark(formTextBox));

                    break;

                case OrderElements.MarkInputLabel:

                    htmlDiv.Add(Mark(formTextBox));
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

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formTextBox.BaseId, "Message") : "");
            htmlLabelMessage.Class.Add("form-validation-message");
            htmlLabelMessage.For.Value = htmlTextBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }

        public virtual void Visit(FormTextArea formTextArea, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formTextArea.BaseId : "");
            htmlDiv.Class.Add("form-textarea");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formTextArea.FormId));
            htmlDiv.Class.Add("form-field");

            if (initialize)
            {
                htmlDiv.Class.Add(formTextArea.IsRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (formTextArea.HasValue)
                    htmlDiv.Class.Add(formTextArea.IsValid ? "form-valid" : "form-invalid");
                else
                    htmlDiv.Class.Add(formTextArea.IsRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formTextArea.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlTextArea htmlTextArea = new HtmlTextArea(formTextArea.BaseId);
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

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? formTextArea.BaseId : "");
            htmlLabel.For.Value = htmlTextArea.Id.Value;
            htmlLabel.Add(new HtmlText(formTextArea.Label));

            switch (formTextArea.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(Mark(formTextArea));
                    htmlDiv.Add(htmlTextArea);

                    break;

                case OrderElements.MarkLabelInput:

                    htmlDiv.Add(Mark(formTextArea));
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextArea);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlTextArea);
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(Mark(formTextArea));

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlTextArea);
                    htmlDiv.Add(Mark(formTextArea));
                    htmlDiv.Add(htmlLabel);

                    break;

                case OrderElements.LabelInputMark:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextArea);
                    htmlDiv.Add(Mark(formTextArea));

                    break;

                case OrderElements.MarkInputLabel:

                    htmlDiv.Add(Mark(formTextArea));
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

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formTextArea.BaseId, "Message") : "");
            htmlLabelMessage.Class.Add("form-validation-message");
            htmlLabelMessage.For.Value = htmlTextArea.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
