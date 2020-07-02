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
            HtmlDiv htmlDiv = new HtmlDiv(formPasswordBox.BaseId);
            htmlDiv.Class.Add("form-password");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formPasswordBox.FormId));
            htmlDiv.Class.Add("form-field");
            
            bool isRequired = formPasswordBox.IsRequired;

            if (!validate)
            {
                htmlDiv.Class.Add(isRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (!string.IsNullOrEmpty(formPasswordBox.Value))
                    htmlDiv.Class.Add(formPasswordBox.IsValid ? "form-valid" : "form-invalid");
                else
                    htmlDiv.Class.Add(isRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formPasswordBox.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlPasswordBox htmlPasswordBox = new HtmlPasswordBox(formPasswordBox.BaseId);
            htmlPasswordBox.Disabled.Value = formPasswordBox.IsDisabled;
            htmlPasswordBox.ReadOnly.Value = formPasswordBox.IsReadOnly;
            htmlPasswordBox.Value.Value = formPasswordBox.Value;
            htmlPasswordBox.PlaceHolder.Value = !string.IsNullOrEmpty(formPasswordBox.PlaceHolder) ? formPasswordBox.PlaceHolder : null;

            HtmlLabel htmlLabel = new HtmlLabel(formPasswordBox.BaseId);
            htmlLabel.For.Value = htmlPasswordBox.Id.Value;

            switch (formPasswordBox.ElementOrder)
            {
                case ElementOrder.LabelMarkInput:

                    htmlLabel.Add(new HtmlText(formPasswordBox.Label));

                    if (isRequired && formPasswordBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formPasswordBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlPasswordBox);

                    break;

                case ElementOrder.MarkLabelInput:

                    if (isRequired && formPasswordBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formPasswordBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formPasswordBox.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlPasswordBox);

                    break;

                case ElementOrder.InputLabelMark:

                    htmlLabel.Add(new HtmlText(formPasswordBox.Label));

                    if (isRequired && formPasswordBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formPasswordBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlPasswordBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.InputMarkLabel:

                    if (isRequired && formPasswordBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formPasswordBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formPasswordBox.Label));

                    htmlDiv.Add(htmlPasswordBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.LabelInputMark:

                    htmlLabel.Add(new HtmlText(formPasswordBox.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlPasswordBox);

                    if (isRequired && formPasswordBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formPasswordBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    break;

                case ElementOrder.MarkInputLabel:

                    if (isRequired && formPasswordBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formPasswordBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formPasswordBox.Label));

                    htmlDiv.Add(htmlPasswordBox);
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
                if (sessionState[formPasswordBox.SessionKey] == null)
                    return;

                formPasswordBox.Content = (string)sessionState[formPasswordBox.SessionKey];
            }

            string message = null;

            if (!formPasswordBox.IsRequiredMet)
                message = formPasswordBox.RequiredMessage;
            else if (!formPasswordBox.IsValid)
                message = formPasswordBox.ValidationMessage;

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formPasswordBox.BaseId, "Message"));
            htmlLabelMessage.Class.Add("form-validation-message");
            htmlLabelMessage.For.Value = htmlPasswordBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
