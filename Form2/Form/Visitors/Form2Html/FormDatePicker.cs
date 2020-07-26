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
    public partial class Form2HtmlVisitor
    {
        public virtual void Visit(FormDatePicker formDatePicker, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formDatePicker.BaseId : "");
            htmlDiv.Class.Add("form-datepicker");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formDatePicker.FormId));
            htmlDiv.Class.Add("form-field");

            if (initialize)
            {
                htmlDiv.Class.Add(formDatePicker.IsRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (!formDatePicker.IsRequired || formDatePicker.HasValue)
                    htmlDiv.Class.Add(formDatePicker.IsValid ? "form-valid" : "form-invalid");
                else
                    htmlDiv.Class.Add(formDatePicker.IsRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formDatePicker.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlDatePicker htmlDatePicker = new HtmlDatePicker(formDatePicker.BaseId);
            htmlDatePicker.Disabled.Value = formDatePicker.IsDisabled;
            htmlDatePicker.ReadOnly.Value = formDatePicker.IsReadOnly;
            htmlDatePicker.DataDateFormat.Value = formDatePicker.DateFormat;
            htmlDatePicker.Value.Value = formDatePicker.HasValue ? formDatePicker.Value.ToString(formDatePicker.DateFormat.Replace('m', 'M'), CultureInfo.InvariantCulture) : "";
            htmlDatePicker.Placeholder.Value = !string.IsNullOrEmpty(formDatePicker.Placeholder) ? formDatePicker.Placeholder : null;
            htmlDatePicker.AutoComplete.Value = "off";
            if (formDatePicker.IsReadOnly)
                htmlDatePicker.DataProvide.Value = null;

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? formDatePicker.BaseId : "");
            htmlLabel.For.Value = htmlDatePicker.Id.Value;
            htmlLabel.Add(new HtmlText(formDatePicker.Label));

            switch (formDatePicker.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(Mark(formDatePicker));
                    htmlDiv.Add(htmlDatePicker);

                    break;

                case OrderElements.MarkLabelInput:

                    htmlDiv.Add(Mark(formDatePicker));
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlDatePicker);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlDatePicker);
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(Mark(formDatePicker));

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlDatePicker);
                    htmlDiv.Add(Mark(formDatePicker));
                    htmlDiv.Add(htmlLabel);

                    break;

                case OrderElements.LabelInputMark:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlDatePicker);
                    htmlDiv.Add(Mark(formDatePicker));

                    break;

                case OrderElements.MarkInputLabel:

                    htmlDiv.Add(Mark(formDatePicker));
                    htmlDiv.Add(htmlDatePicker);
                    htmlDiv.Add(htmlLabel);

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

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formDatePicker.BaseId, "Message") : "");
            htmlLabelMessage.Class.Add("form-validation-message");
            htmlLabelMessage.For.Value = htmlDatePicker.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
