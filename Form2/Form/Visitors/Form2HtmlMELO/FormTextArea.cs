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
        public virtual void Visit(FormTextArea formTextArea, HtmlContainer htmlContainer)
        {
            HtmlFieldset htmlFieldset = verbose ? new HtmlFieldset(formTextArea.Path) : new HtmlFieldset();

            htmlFieldset.Class.Add("formTextArea");

            if (!string.IsNullOrWhiteSpace(formTextArea.CssClass))
                htmlFieldset.Class.AddRange(formTextArea.CssClass.Split(' ').Where(s => s != string.Empty));

            htmlFieldset.Class.Add("form-group");

            if (!string.IsNullOrWhiteSpace(formTextArea.Path))
                htmlFieldset.Class.Add(string.Format("{0}{1}", "formId", formTextArea.Path));

            htmlFieldset.Class.Add("formField");

            if (initialize)
            {
                htmlFieldset.Class.Add(formTextArea.IsRequired ? "formRequired" : "formOptional");
            }
            else
            {
                if (formTextArea.HasValue)
                    htmlFieldset.Class.Add(formTextArea.IsValid ? "formValid" : "formInvalid");
                else
                    htmlFieldset.Class.Add(formTextArea.IsRequired ? "formNotEntered" : "formOptional");
            }

            htmlFieldset.Hidden.Value = formTextArea.IsHidden;

            htmlContainer.Add(htmlFieldset);

            HtmlTextArea htmlTextArea = new HtmlTextArea(formTextArea.Path);
            htmlTextArea.Class.Add("form-control");
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

                    AddLabelMark(formTextArea, htmlTextArea, htmlFieldset);
                    htmlFieldset.Add(htmlTextArea);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMarkLabel(formTextArea, htmlTextArea, htmlFieldset);
                    htmlFieldset.Add(htmlTextArea);

                    break;

                case OrderElements.InputLabelMark:

                    htmlFieldset.Add(htmlTextArea);
                    AddLabelMark(formTextArea, htmlTextArea, htmlFieldset);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlFieldset.Add(htmlTextArea);
                    AddMarkLabel(formTextArea, htmlTextArea, htmlFieldset);

                    break;

                case OrderElements.LabelInputMark:

                    AddLabel(formTextArea, htmlTextArea, htmlFieldset);
                    htmlFieldset.Add(htmlTextArea);
                    AddMark(formTextArea, htmlTextArea, htmlFieldset);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formTextArea, htmlTextArea, htmlFieldset);
                    htmlFieldset.Add(htmlTextArea);
                    AddLabel(formTextArea, htmlTextArea, htmlFieldset);

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
            htmlFieldset.Add(htmlLabelMessage);
        }
    }
}
