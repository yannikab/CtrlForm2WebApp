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
    public partial class Form2HtmlMELOVisitor
    {
        public virtual void Visit(FormTextBox formTextBox, HtmlContainer htmlContainer)
        {
            HtmlFieldset htmlFieldset = verbose ? new HtmlFieldset(formTextBox.Path) : new HtmlFieldset();

            htmlFieldset.Class.Add("formTextBox");

            if (!string.IsNullOrWhiteSpace(formTextBox.CssClass))
                htmlFieldset.Class.AddRange(formTextBox.CssClass.Split(' ').Where(s => s != string.Empty));

            htmlFieldset.Class.Add("form-group");

            if (!string.IsNullOrWhiteSpace(formTextBox.Path))
                htmlFieldset.Class.Add(string.Format("{0}{1}", "formId", formTextBox.Path));

            htmlFieldset.Class.Add("formField");

            if (initialize)
            {
                htmlFieldset.Class.Add(formTextBox.IsRequired ? "formRequired" : "formOptional");
            }
            else
            {
                if (formTextBox.HasValue)
                    htmlFieldset.Class.Add(formTextBox.IsValid ? "formValid" : "formInvalid");
                else
                    htmlFieldset.Class.Add(formTextBox.IsRequired ? "formNotEntered" : "formOptional");
            }

            htmlFieldset.Hidden.Value = formTextBox.IsHidden;

            htmlContainer.Add(htmlFieldset);

            HtmlTextBox htmlTextBox = new HtmlTextBox(formTextBox.Path);
            htmlTextBox.Class.Add("form-control");
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

            if (!initialize && firstInvalidId == null)
                if (formTextBox.IsRequired && !formTextBox.HasValue || !formTextBox.IsValid)
                    firstInvalidId = htmlTextBox.Id.Value;

            switch (formTextBox.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    AddLabelMark(formTextBox, htmlTextBox, htmlFieldset);
                    htmlFieldset.Add(htmlTextBox);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMarkLabel(formTextBox, htmlTextBox, htmlFieldset);
                    htmlFieldset.Add(htmlTextBox);

                    break;

                case OrderElements.InputLabelMark:

                    htmlFieldset.Add(htmlTextBox);
                    AddLabelMark(formTextBox, htmlTextBox, htmlFieldset);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlFieldset.Add(htmlTextBox);
                    AddMarkLabel(formTextBox, htmlTextBox, htmlFieldset);

                    break;

                case OrderElements.LabelInputMark:

                    AddLabel(formTextBox, htmlTextBox, htmlFieldset);
                    htmlFieldset.Add(htmlTextBox);
                    AddMark(formTextBox, htmlTextBox, htmlFieldset);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formTextBox, htmlTextBox, htmlFieldset);
                    htmlFieldset.Add(htmlTextBox);
                    AddLabel(formTextBox, htmlTextBox, htmlFieldset);

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

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formTextBox.Path, "Message") : "");
            htmlLabelMessage.Class.Add("formValidationMessage");
            htmlLabelMessage.For.Value = htmlTextBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlFieldset.Add(htmlLabelMessage);
        }
    }
}
