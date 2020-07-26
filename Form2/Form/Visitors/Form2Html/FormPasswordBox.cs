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
        public virtual void Visit(FormPasswordBox formPasswordBox, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formPasswordBox.BaseId : "");
            htmlDiv.Class.Add("form-password");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formPasswordBox.FormId));
            htmlDiv.Class.Add("form-field");

            if (initialize)
            {
                htmlDiv.Class.Add(formPasswordBox.IsRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (!string.IsNullOrEmpty(formPasswordBox.Value))
                    htmlDiv.Class.Add(formPasswordBox.IsValid ? "form-valid" : "form-invalid");
                else
                    htmlDiv.Class.Add(formPasswordBox.IsRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formPasswordBox.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlPasswordBox htmlPasswordBox = new HtmlPasswordBox(formPasswordBox.BaseId);
            htmlPasswordBox.Disabled.Value = formPasswordBox.IsDisabled;
            htmlPasswordBox.ReadOnly.Value = formPasswordBox.IsReadOnly;
            htmlPasswordBox.Value.Value = formPasswordBox.Value;
            
            string placeholder = null;

            if (!string.IsNullOrWhiteSpace(formPasswordBox.Placeholder))
            {
                if (formPasswordBox.IsRequired && formPasswordBox.IsRequiredInPlaceholder && !string.IsNullOrWhiteSpace(formPasswordBox.RequiredMark))
                    placeholder = string.Format("{0} {1}", formPasswordBox.Placeholder, formPasswordBox.RequiredMark);
                else if (!formPasswordBox.IsRequired && formPasswordBox.IsOptionalInPlaceholder && !string.IsNullOrWhiteSpace(formPasswordBox.OptionalMark))
                    placeholder = string.Format("{0} {1}", formPasswordBox.Placeholder, formPasswordBox.OptionalMark);
                else
                    placeholder = formPasswordBox.Placeholder;
            }

            htmlPasswordBox.Placeholder.Value = placeholder;

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? formPasswordBox.BaseId : "");
            htmlLabel.For.Value = htmlPasswordBox.Id.Value;
            htmlLabel.Add(new HtmlText(formPasswordBox.Label));

            switch (formPasswordBox.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(Mark(formPasswordBox));
                    htmlDiv.Add(htmlPasswordBox);

                    break;

                case OrderElements.MarkLabelInput:

                    htmlDiv.Add(Mark(formPasswordBox));
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlPasswordBox);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlPasswordBox);
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(Mark(formPasswordBox));

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlPasswordBox);
                    htmlDiv.Add(Mark(formPasswordBox));
                    htmlDiv.Add(htmlLabel);

                    break;

                case OrderElements.LabelInputMark:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlPasswordBox);
                    htmlDiv.Add(Mark(formPasswordBox));

                    break;

                case OrderElements.MarkInputLabel:

                    htmlDiv.Add(Mark(formPasswordBox));
                    htmlDiv.Add(htmlPasswordBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                default:
                case OrderElements.NotSet:

                    break;
            }

            if (initialize)
                return;

            string message = null;

            if (formPasswordBox.UseLastMessage)
            {
                if (!string.IsNullOrEmpty(formPasswordBox.LastMessage))
                    message = formPasswordBox.LastMessage;
            }
            else if (formPasswordBox.IsRequired && !formPasswordBox.HasValue)
            {
                message = formPasswordBox.RequiredMessage;
            }
            else if (!formPasswordBox.IsValid)
            {
                message = formPasswordBox.ValidationMessage;
            }

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formPasswordBox.BaseId, "Message") : "");
            htmlLabelMessage.Class.Add("form-validation-message");
            htmlLabelMessage.For.Value = htmlPasswordBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
