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
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formPasswordBox.Path : "");
            htmlDiv.Class.Add("formPasswordBox");
            if (!string.IsNullOrWhiteSpace(formPasswordBox.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formPasswordBox.Path));
            htmlDiv.Class.Add("formField");

            if (initialize)
            {
                htmlDiv.Class.Add(formPasswordBox.IsRequired ? "formRequired" : "formOptional");
            }
            else
            {
                if (!string.IsNullOrEmpty(formPasswordBox.Value))
                    htmlDiv.Class.Add(formPasswordBox.IsValid ? "formValid" : "formInvalid");
                else
                    htmlDiv.Class.Add(formPasswordBox.IsRequired ? "formNotEntered" : "formOptional");
            }

            htmlDiv.Hidden.Value = formPasswordBox.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlPasswordBox htmlPasswordBox = new HtmlPasswordBox(formPasswordBox.Path);
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

            switch (formPasswordBox.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    AddLabelMark(formPasswordBox, htmlPasswordBox, htmlDiv);
                    htmlDiv.Add(htmlPasswordBox);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMarkLabel(formPasswordBox, htmlPasswordBox, htmlDiv);
                    htmlDiv.Add(htmlPasswordBox);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlPasswordBox);
                    AddLabelMark(formPasswordBox, htmlPasswordBox, htmlDiv);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlPasswordBox);
                    AddMarkLabel(formPasswordBox, htmlPasswordBox, htmlDiv);

                    break;

                case OrderElements.LabelInputMark:

                    AddLabel(formPasswordBox, htmlPasswordBox, htmlDiv);
                    htmlDiv.Add(htmlPasswordBox);
                    AddMark(formPasswordBox, htmlPasswordBox, htmlDiv);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formPasswordBox, htmlPasswordBox, htmlDiv);
                    htmlDiv.Add(htmlPasswordBox);
                    AddLabel(formPasswordBox, htmlPasswordBox, htmlDiv);

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

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formPasswordBox.Path, "Message") : "");
            htmlLabelMessage.Class.Add("formValidationMessage");
            htmlLabelMessage.For.Value = htmlPasswordBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
