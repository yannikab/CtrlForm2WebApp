using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Content.Items.Input;
using CtrlForm2.Form.Enums;
using CtrlForm2.Html.Content;
using CtrlForm2.Html.Content.Elements;
using CtrlForm2.Html.Content.Elements.Containers;
using CtrlForm2.Html.Content.Elements.Input;

namespace CtrlForm2.Form.Visitors
{
    public partial class Form2HtmlVisitor
    {
        public virtual void Visit(FormDatePicker formDatePicker, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formDatePicker.BaseId);
            htmlDiv.Class.Add("form-datepicker");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formDatePicker.FormId));

            bool isRequired = formDatePicker.IsRequired ?? false;

            if (!IsPostBack)
            {
                htmlDiv.Class.Add(isRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (formDatePicker.IsEntered)
                    htmlDiv.Class.Add(formDatePicker.IsValid ? "form-valid" : "form-invalid");
                else
                    htmlDiv.Class.Add(isRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formDatePicker.IsHidden;
            htmlContainer.Add(htmlDiv);

            HtmlDatePicker htmlDatePicker = new HtmlDatePicker(formDatePicker.BaseId);
            htmlDatePicker.Hidden.Value = formDatePicker.IsHidden;
            htmlDatePicker.ReadOnly.Value = formDatePicker.IsReadOnly;
            htmlDatePicker.Value.Value = formDatePicker.IsEntered ? ((DateTime)formDatePicker.Date).ToString("yyyy-MM-dd") : "";
            htmlDatePicker.Disabled.Value = formDatePicker.IsDisabled;

            HtmlLabel htmlLabel = new HtmlLabel(formDatePicker.BaseId);
            htmlLabel.Hidden.Value = formDatePicker.IsHidden;
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

            if (!IsPostBack)
                return;

            string message = null;

            if (isRequired && !formDatePicker.IsEntered)
                message = formDatePicker.RequiredMessage;
            else if ((isRequired || formDatePicker.IsEntered) && !formDatePicker.IsValid)
                message = formDatePicker.ValidationMessage;

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formDatePicker.BaseId, "Message"));
            htmlLabelMessage.Hidden.Value = formDatePicker.IsHidden;
            htmlLabelMessage.For.Value = htmlDatePicker.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
