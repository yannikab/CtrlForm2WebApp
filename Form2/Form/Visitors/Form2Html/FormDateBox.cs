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

            bool isRequired = formDateBox.IsRequired;

            if (!validate)
            {
                htmlDiv.Class.Add(isRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (formDateBox.IsRequiredMet)
                    htmlDiv.Class.Add(formDateBox.IsValid ? "form-valid" : "form-invalid");
                else
                    htmlDiv.Class.Add(isRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formDateBox.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlDateBox htmlDateBox = new HtmlDateBox(formDateBox.BaseId);
            htmlDateBox.Disabled.Value = formDateBox.IsDisabled;
            htmlDateBox.ReadOnly.Value = formDateBox.IsReadOnly;
            htmlDateBox.Value.Value = formDateBox.IsRequiredMet ? ((DateTime)formDateBox.Value).ToString("yyyy-MM-dd") : "";

            HtmlLabel htmlLabel = new HtmlLabel(formDateBox.BaseId);
            htmlLabel.For.Value = htmlDateBox.Id.Value;

            switch (formDateBox.ElementOrder)
            {
                case ElementOrder.LabelMarkInput:

                    htmlLabel.Add(new HtmlText(formDateBox.Label));

                    if (isRequired && formDateBox.RequiredMark != null)
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

                    if (isRequired && formDateBox.RequiredMark != null)
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

                    if (isRequired && formDateBox.RequiredMark != null)
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

                    if (isRequired && formDateBox.RequiredMark != null)
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

                    if (isRequired && formDateBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formDateBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    break;

                case ElementOrder.MarkInputLabel:

                    if (isRequired && formDateBox.RequiredMark != null)
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

            if (!validate)
                return;

            if (sessionState != null)
            {
                if (sessionState[formDateBox.SessionKey] == null)
                    return;

                formDateBox.Content = (string)sessionState[formDateBox.SessionKey];
            }

            string message = null;

            if (!formDateBox.IsRequiredMet)
                message = formDateBox.RequiredMessage;
            else if (!formDateBox.IsValid)
                message = formDateBox.ValidationMessage;

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formDateBox.BaseId, "Message"));
            htmlLabelMessage.For.Value = htmlDateBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
