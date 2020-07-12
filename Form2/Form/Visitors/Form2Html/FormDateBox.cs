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
        public virtual void Visit(FormDateBox formDateBox, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formDateBox.BaseId);
            htmlDiv.Class.Add("form-datebox");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formDateBox.FormId));
            htmlDiv.Class.Add("form-field");

            if (initialize)
            {
                htmlDiv.Class.Add(formDateBox.IsRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (!formDateBox.IsRequired || formDateBox.HasValue)
                    htmlDiv.Class.Add(formDateBox.IsValid ? "form-valid" : "form-invalid");
                else
                    htmlDiv.Class.Add(formDateBox.IsRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formDateBox.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlDateBox htmlDateBox = new HtmlDateBox(formDateBox.BaseId);
            htmlDateBox.Disabled.Value = formDateBox.IsDisabled;
            htmlDateBox.ReadOnly.Value = formDateBox.IsReadOnly;
            htmlDateBox.Value.Value = formDateBox.HasValue ? formDateBox.Value.ToString("yyyy-MM-dd") : "";

            HtmlLabel htmlLabel = new HtmlLabel(formDateBox.BaseId);
            htmlLabel.For.Value = htmlDateBox.Id.Value;

            switch (formDateBox.ElementOrder)
            {
                case ElementOrder.LabelMarkInput:

                    htmlLabel.Add(new HtmlText(formDateBox.Label));

                    if (formDateBox.IsRequired && !string.IsNullOrWhiteSpace(formDateBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formDateBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlDateBox);

                    break;

                case ElementOrder.MarkLabelInput:

                    if (formDateBox.IsRequired && !string.IsNullOrWhiteSpace(formDateBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formDateBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formDateBox.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlDateBox);

                    break;

                case ElementOrder.InputLabelMark:

                    htmlLabel.Add(new HtmlText(formDateBox.Label));

                    if (formDateBox.IsRequired && !string.IsNullOrWhiteSpace(formDateBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formDateBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlDateBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.InputMarkLabel:

                    if (formDateBox.IsRequired && !string.IsNullOrWhiteSpace(formDateBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formDateBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formDateBox.Label));

                    htmlDiv.Add(htmlDateBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.LabelInputMark:

                    htmlLabel.Add(new HtmlText(formDateBox.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlDateBox);

                    if (formDateBox.IsRequired && !string.IsNullOrWhiteSpace(formDateBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formDateBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    break;

                case ElementOrder.MarkInputLabel:

                    if (formDateBox.IsRequired && !string.IsNullOrWhiteSpace(formDateBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formDateBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formDateBox.Label));

                    htmlDiv.Add(htmlDateBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                default:
                case ElementOrder.NotSet:

                    break;
            }

            if (initialize)
                return;

            string message = null;

            if (formDateBox.UseLastMessage)
            {
                if (!string.IsNullOrEmpty(formDateBox.LastMessage))
                    message = formDateBox.LastMessage;
            }
            else if (formDateBox.IsRequired && !formDateBox.HasValue)
            {
                message = formDateBox.RequiredMessage;
            }
            else if (!formDateBox.IsValid)
            {
                message = formDateBox.ValidationMessage;
            }

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formDateBox.BaseId, "Message"));
            htmlLabelMessage.For.Value = htmlDateBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
