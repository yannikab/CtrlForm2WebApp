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
            HtmlDiv htmlDiv = new HtmlDiv(formDatePicker.BaseId);
            htmlDiv.Class.Add("form-datepicker");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formDatePicker.FormId));

            bool isRequired = formDatePicker.IsRequired ?? false;

            if (!validate)
            {
                htmlDiv.Class.Add(isRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (formDatePicker.IsRequiredMet)
                    htmlDiv.Class.Add(formDatePicker.IsValid ? "form-valid" : "form-invalid");
                else
                    htmlDiv.Class.Add(isRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formDatePicker.IsHidden;
            htmlContainer.Add(htmlDiv);

            HtmlDatePicker htmlDatePicker = new HtmlDatePicker(formDatePicker.BaseId);
            htmlDatePicker.Disabled.Value = formDatePicker.IsDisabled;
            htmlDatePicker.ReadOnly.Value = formDatePicker.IsReadOnly;
            htmlDatePicker.DataDateFormat.Value = formDatePicker.DateFormat;
            if (!formDatePicker.IsRequiredMet || !formDatePicker.Value.HasValue)
                htmlDatePicker.Value.Value = "";
            else
                htmlDatePicker.Value.Value = formDatePicker.Value.Value.ToString(formDatePicker.DateFormat.Replace('m', 'M'), CultureInfo.InvariantCulture);
            htmlDatePicker.PlaceHolder.Value = !string.IsNullOrEmpty(formDatePicker.PlaceHolder) ? formDatePicker.PlaceHolder : null;

            HtmlLabel htmlLabel = new HtmlLabel(formDatePicker.BaseId);
            htmlLabel.For.Value = htmlDatePicker.Id.Value;

            switch (formDatePicker.ElementOrder)
            {
                case ElementOrder.LabelMarkInput:

                    htmlLabel.Add(new HtmlText(formDatePicker.Label));

                    if (isRequired && formDatePicker.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formDatePicker.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlDatePicker);

                    break;

                case ElementOrder.MarkLabelInput:

                    if (isRequired && formDatePicker.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formDatePicker.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formDatePicker.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlDatePicker);

                    break;

                case ElementOrder.InputLabelMark:

                    htmlLabel.Add(new HtmlText(formDatePicker.Label));

                    if (isRequired && formDatePicker.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formDatePicker.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlDatePicker);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.InputMarkLabel:

                    if (isRequired && formDatePicker.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formDatePicker.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formDatePicker.Label));

                    htmlDiv.Add(htmlDatePicker);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.LabelInputMark:

                    htmlLabel.Add(new HtmlText(formDatePicker.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlDatePicker);

                    if (isRequired && formDatePicker.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formDatePicker.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    break;

                case ElementOrder.MarkInputLabel:

                    if (isRequired && formDatePicker.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formDatePicker.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formDatePicker.Label));

                    htmlDiv.Add(htmlDatePicker);
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
                if (sessionState[formDatePicker.SessionKey] == null)
                    return;

                formDatePicker.Content = (string)sessionState[formDatePicker.SessionKey];
            }

            string message = null;

            if (!formDatePicker.IsRequiredMet)
                message = formDatePicker.RequiredMessage;
            else if (!formDatePicker.IsValid)
                message = formDatePicker.ValidationMessage;

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formDatePicker.BaseId, "Message"));
            htmlLabelMessage.For.Value = htmlDatePicker.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
