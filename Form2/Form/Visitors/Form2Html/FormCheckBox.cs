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
        public virtual void Visit(FormCheckBox formCheckBox, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formCheckBox.BaseId);
            htmlDiv.Class.Add("form-checkbox");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formCheckBox.FormId));
            htmlDiv.Class.Add("form-field");

            if (initialize)
            {
                htmlDiv.Class.Add(formCheckBox.IsRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (!formCheckBox.IsRequired || formCheckBox.Value)
                    htmlDiv.Class.Add("form-valid");
                else
                    htmlDiv.Class.Add(formCheckBox.IsRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formCheckBox.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlCheckBox htmlCheckBox = new HtmlCheckBox(formCheckBox.BaseId);
            htmlCheckBox.Disabled.Value = formCheckBox.IsDisabled;
            htmlCheckBox.Checked.Value = formCheckBox.Value;

            HtmlLabel htmlLabel = new HtmlLabel(formCheckBox.BaseId);
            htmlLabel.For.Value = htmlCheckBox.Id.Value;

            switch (formCheckBox.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    htmlLabel.Add(new HtmlText(formCheckBox.Label));

                    if (formCheckBox.IsRequired && !string.IsNullOrWhiteSpace(formCheckBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formCheckBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlCheckBox);

                    break;

                case OrderElements.MarkLabelInput:

                    if (formCheckBox.IsRequired && !string.IsNullOrWhiteSpace(formCheckBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formCheckBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formCheckBox.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlCheckBox);

                    break;

                case OrderElements.InputLabelMark:

                    htmlLabel.Add(new HtmlText(formCheckBox.Label));

                    if (formCheckBox.IsRequired && !string.IsNullOrWhiteSpace(formCheckBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formCheckBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlCheckBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                case OrderElements.InputMarkLabel:

                    if (formCheckBox.IsRequired && !string.IsNullOrWhiteSpace(formCheckBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formCheckBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formCheckBox.Label));

                    htmlDiv.Add(htmlCheckBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                case OrderElements.LabelInputMark:

                    htmlLabel.Add(new HtmlText(formCheckBox.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlCheckBox);

                    if (formCheckBox.IsRequired && !string.IsNullOrWhiteSpace(formCheckBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formCheckBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    break;

                case OrderElements.MarkInputLabel:

                    if (formCheckBox.IsRequired && !string.IsNullOrWhiteSpace(formCheckBox.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formCheckBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formCheckBox.Label));

                    htmlDiv.Add(htmlCheckBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                default:
                case OrderElements.NotSet:

                    break;
            }

            if (initialize)
                return;

            string message = null;
            
            if (formCheckBox.UseLastMessage)
            {
                if (!string.IsNullOrEmpty(formCheckBox.LastMessage))
                    message = formCheckBox.LastMessage;
            }
            else if (formCheckBox.IsRequired && !formCheckBox.Value)
            {
                message = formCheckBox.RequiredMessage;
            }
            else if (!formCheckBox.IsValid)
            {
                message = formCheckBox.ValidationMessage;
            }

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formCheckBox.BaseId, "Message"));
            htmlLabelMessage.Class.Add("form-validation-message"); 
            htmlLabelMessage.For.Value = htmlCheckBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(formCheckBox.RequiredMessage));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
