using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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
        public virtual void Visit(FormDatePicker formDatePicker, HtmlContainer htmlContainer)
        {
            HtmlFieldset htmlFieldset = verbose ? new HtmlFieldset(formDatePicker.Path) : new HtmlFieldset();

            htmlFieldset.Class.Add("formDatePicker");

            if (!string.IsNullOrWhiteSpace(formDatePicker.CssClass))
                htmlFieldset.Class.AddRange(formDatePicker.CssClass.Split(' ').Where(s => s != string.Empty));

            htmlFieldset.Class.Add("form-group");

            if (!string.IsNullOrWhiteSpace(formDatePicker.Path))
                htmlFieldset.Class.Add(string.Format("{0}{1}", "formId", formDatePicker.Path));

            htmlFieldset.Class.Add("formField");

            if (initialize)
            {
                htmlFieldset.Class.Add(formDatePicker.IsRequired ? "formRequired" : "formOptional");
            }
            else
            {
                if (!formDatePicker.IsRequired || formDatePicker.HasValue)
                    htmlFieldset.Class.Add(formDatePicker.IsValid ? "formValid" : "formInvalid");
                else
                    htmlFieldset.Class.Add(formDatePicker.IsRequired ? "formNotEntered" : "formOptional");
            }

            htmlFieldset.Hidden.Value = formDatePicker.IsHidden;

            htmlContainer.Add(htmlFieldset);

            HtmlDatePicker htmlDatePicker = new HtmlDatePicker(formDatePicker.Path);
            htmlDatePicker.Class.Add("form-control");
            htmlDatePicker.Disabled.Value = formDatePicker.IsDisabled;
            htmlDatePicker.ReadOnly.Value = formDatePicker.IsReadOnly;
            htmlDatePicker.DataDateFormat.Value = formDatePicker.DateFormat;
            htmlDatePicker.Value.Value = formDatePicker.HasValue ? formDatePicker.Value.ToString(formDatePicker.DateFormat.Replace('m', 'M'), CultureInfo.InvariantCulture) : "";
            htmlDatePicker.Placeholder.Value = !string.IsNullOrEmpty(formDatePicker.Placeholder) ? formDatePicker.Placeholder : null;
            htmlDatePicker.AutoComplete.Value = "off";
            if (formDatePicker.IsReadOnly)
                htmlDatePicker.DataProvide.Value = null;

            if (!initialize && firstInvalidId == null)
                if (formDatePicker.IsRequired && !formDatePicker.HasValue || !formDatePicker.IsValid)
                    firstInvalidId = htmlDatePicker.Id.Value;

            switch (formDatePicker.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    AddLabelMark(formDatePicker, htmlDatePicker, htmlFieldset);
                    htmlFieldset.Add(htmlDatePicker);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMarkLabel(formDatePicker, htmlDatePicker, htmlFieldset);
                    htmlFieldset.Add(htmlDatePicker);

                    break;

                case OrderElements.InputLabelMark:

                    htmlFieldset.Add(htmlDatePicker);
                    AddLabelMark(formDatePicker, htmlDatePicker, htmlFieldset);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlFieldset.Add(htmlDatePicker);
                    AddMarkLabel(formDatePicker, htmlDatePicker, htmlFieldset);

                    break;

                case OrderElements.LabelInputMark:

                    AddLabel(formDatePicker, htmlDatePicker, htmlFieldset);
                    htmlFieldset.Add(htmlDatePicker);
                    AddMark(formDatePicker, htmlDatePicker, htmlFieldset);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formDatePicker, htmlDatePicker, htmlFieldset);
                    htmlFieldset.Add(htmlDatePicker);
                    AddLabel(formDatePicker, htmlDatePicker, htmlFieldset);

                    break;

                default:
                case OrderElements.NotSet:

                    break;
            }

            if (initialize)
                return;

            string message = null;

            if (formDatePicker.UseLastMessage)
            {
                if (!string.IsNullOrEmpty(formDatePicker.LastMessage))
                    message = formDatePicker.LastMessage;
            }
            else if (formDatePicker.IsRequired && !formDatePicker.HasValue)
            {
                message = formDatePicker.RequiredMessage;
            }
            else if (!formDatePicker.IsValid)
            {
                message = formDatePicker.ValidationMessage;
            }

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formDatePicker.Path, "Message") : "");
            htmlLabelMessage.Class.Add("formValidationMessage");
            htmlLabelMessage.For.Value = htmlDatePicker.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlFieldset.Add(htmlLabelMessage);
        }
    }
}
