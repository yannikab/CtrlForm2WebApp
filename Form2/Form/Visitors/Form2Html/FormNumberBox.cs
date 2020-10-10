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
        public virtual void Visit(FormNumberBox formNumberBox, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formNumberBox.Path : "");
            htmlDiv.Class.Add("formNumberBox");
            if (!string.IsNullOrWhiteSpace(formNumberBox.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formNumberBox.Path));
            htmlDiv.Class.Add("formField");

            if (initialize)
            {
                htmlDiv.Class.Add(formNumberBox.IsRequired ? "formRequired" : "formOptional");
            }
            else
            {
                if (!formNumberBox.IsRequired || formNumberBox.HasValue)
                    htmlDiv.Class.Add(formNumberBox.IsValid ? "formValid" : "formInvalid");
                else
                    htmlDiv.Class.Add(formNumberBox.IsRequired ? "formNotEntered" : "formOptional");
            }

            htmlDiv.Hidden.Value = formNumberBox.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlNumberBox htmlNumberBox = new HtmlNumberBox(formNumberBox.Path);
            htmlNumberBox.Disabled.Value = formNumberBox.IsDisabled;
            htmlNumberBox.ReadOnly.Value = formNumberBox.IsReadOnly;
            htmlNumberBox.Value.Value = formNumberBox.Value.ToString();
            htmlNumberBox.Min.Value = formNumberBox.Min;
            htmlNumberBox.Max.Value = formNumberBox.Max;
            htmlNumberBox.Step.Value = formNumberBox.Step;

            string placeholder = null;

            if (!string.IsNullOrWhiteSpace(formNumberBox.Placeholder))
            {
                if (formNumberBox.IsRequired && formNumberBox.IsRequiredInPlaceholder && !string.IsNullOrWhiteSpace(formNumberBox.RequiredMark))
                    placeholder = string.Format("{0} {1}", formNumberBox.Placeholder, formNumberBox.RequiredMark);
                else if (!formNumberBox.IsRequired && formNumberBox.IsOptionalInPlaceholder && !string.IsNullOrWhiteSpace(formNumberBox.OptionalMark))
                    placeholder = string.Format("{0} {1}", formNumberBox.Placeholder, formNumberBox.OptionalMark);
                else
                    placeholder = formNumberBox.Placeholder;
            }

            htmlNumberBox.Placeholder.Value = placeholder;

            switch (formNumberBox.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    AddLabelMark(formNumberBox, htmlNumberBox, htmlDiv);
                    htmlDiv.Add(htmlNumberBox);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMarkLabel(formNumberBox, htmlNumberBox, htmlDiv);
                    htmlDiv.Add(htmlNumberBox);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlNumberBox);
                    AddLabelMark(formNumberBox, htmlNumberBox, htmlDiv);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlNumberBox);
                    AddMarkLabel(formNumberBox, htmlNumberBox, htmlDiv);

                    break;

                case OrderElements.LabelInputMark:

                    AddLabel(formNumberBox, htmlNumberBox, htmlDiv);
                    htmlDiv.Add(htmlNumberBox);
                    AddMark(formNumberBox, htmlNumberBox, htmlDiv);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formNumberBox, htmlNumberBox, htmlDiv);
                    htmlDiv.Add(htmlNumberBox);
                    AddLabel(formNumberBox, htmlNumberBox, htmlDiv);

                    break;

                default:
                case OrderElements.NotSet:

                    break;
            }

            if (initialize)
                return;

            string message = null;

            if (formNumberBox.UseLastMessage)
            {
                if (!string.IsNullOrEmpty(formNumberBox.LastMessage))
                    message = formNumberBox.LastMessage;
            }
            else if (formNumberBox.IsRequired && !formNumberBox.HasValue)
            {
                message = formNumberBox.RequiredMessage;
            }
            else if (!formNumberBox.IsValid)
            {
                message = formNumberBox.ValidationMessage;
            }

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formNumberBox.Path, "Message"));
            htmlLabelMessage.Class.Add("formValidationMessage");
            htmlLabelMessage.For.Value = htmlNumberBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
