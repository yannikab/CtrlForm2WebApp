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
        public virtual void Visit(FormNumberBox formNumberBox, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formNumberBox.BaseId : "");
            htmlDiv.Class.Add("form-numberbox");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formNumberBox.FormId));
            htmlDiv.Class.Add("form-field");

            if (initialize)
            {
                htmlDiv.Class.Add(formNumberBox.IsRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (formNumberBox.HasValue)
                    htmlDiv.Class.Add(formNumberBox.IsValid ? "form-valid" : "form-invalid");
                else
                    htmlDiv.Class.Add(formNumberBox.IsRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formNumberBox.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlTextBox htmlTextBox = new HtmlTextBox(formNumberBox.BaseId);
            htmlTextBox.Disabled.Value = formNumberBox.IsDisabled;
            htmlTextBox.ReadOnly.Value = formNumberBox.IsReadOnly || !formNumberBox.IsDirectInput;
            htmlTextBox.Value.Value = formNumberBox.HasValue ? formNumberBox.Value.ToString() : "";

            string placeholder = null;

            if (!string.IsNullOrWhiteSpace(formNumberBox.Placeholder))
            {
                if (formNumberBox.IsRequired && formNumberBox.IsRequiredInPlaceholder && !string.IsNullOrWhiteSpace(formNumberBox.RequiredMark))
                    placeholder = string.Format("{0} {1}", formNumberBox.Placeholder, formNumberBox.RequiredMark);
                else if (!formNumberBox.IsRequired && formNumberBox.IsOptionalInPlaceholder && !string.IsNullOrWhiteSpace(formNumberBox.OptionalMark))
                    placeholder = string.Format("{0} {1}", formNumberBox.Placeholder, formNumberBox.OptionalMark);
                else
                    placeholder = formNumberBox.Placeholder;
            }

            htmlTextBox.Placeholder.Value = placeholder;

            htmlTextBox.Change.Value = string.Format("__doPostBack('{0}', '');", formNumberBox.BaseId);

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? formNumberBox.BaseId : "");
            htmlLabel.For.Value = htmlTextBox.Id.Value;
            htmlLabel.Add(new HtmlText(formNumberBox.Label));

            HtmlDiv htmlDivNumberBox = BuildNumberBoxDiv(formNumberBox, htmlTextBox);

            switch (formNumberBox.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(Mark(formNumberBox));
                    htmlDiv.Add(htmlDivNumberBox);

                    break;

                case OrderElements.MarkLabelInput:

                    htmlDiv.Add(Mark(formNumberBox));
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlDivNumberBox);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlDivNumberBox);
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(Mark(formNumberBox));

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlDivNumberBox);
                    htmlDiv.Add(Mark(formNumberBox));
                    htmlDiv.Add(htmlLabel);

                    break;

                case OrderElements.LabelInputMark:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlDivNumberBox);
                    htmlDiv.Add(Mark(formNumberBox));

                    break;

                case OrderElements.MarkInputLabel:

                    htmlDiv.Add(Mark(formNumberBox));
                    htmlDiv.Add(htmlDivNumberBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                default:
                case OrderElements.NotSet:

                    break;
            }

            if (initialize)
                return;

            string message = null;

            if (formNumberBox.UseLastMessage)
            {
                if (!string.IsNullOrEmpty(formNumberBox.LastMessage))
                    message = formNumberBox.LastMessage;
            }
            else if (formNumberBox.IsRequired && !formNumberBox.HasValue)
            {
                message = formNumberBox.RequiredMessage;
            }
            else if (!formNumberBox.IsValid)
            {
                message = formNumberBox.ValidationMessage;
            }

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formNumberBox.BaseId, "Message") : "");
            htmlLabelMessage.Class.Add("form-validation-message");
            htmlLabelMessage.For.Value = htmlTextBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }

        private HtmlDiv BuildNumberBoxDiv(FormNumberBox formNumberBox, HtmlTextBox htmlTextBox)
        {
            HtmlDiv htmlDivNumberBox = new HtmlDiv("");

            string btnDecrName = verbose ? string.Format("{0}{1}", "Decr", formNumberBox.BaseId) : "";
            string btnIncrName = verbose ? string.Format("{0}{1}", "Incr", formNumberBox.BaseId) : "";

            string btnDecrOnClick = string.Format("__doPostBack('{0}', 'Decr');", formNumberBox.BaseId);
            string btnIncrOnClick = string.Format("__doPostBack('{0}', 'Incr');", formNumberBox.BaseId);

            HtmlButton htmlButtonDecr = new HtmlButton(btnDecrName, btnDecrOnClick);
            HtmlButton htmlButtonIncr = new HtmlButton(btnIncrName, btnIncrOnClick);

            htmlButtonDecr.Value.Value = formNumberBox.DecrText;
            htmlButtonIncr.Value.Value = formNumberBox.IncrText;

            htmlButtonDecr.Disabled.Value = htmlButtonIncr.Disabled.Value = formNumberBox.IsReadOnly || !formNumberBox.HasValue;
            
            switch (formNumberBox.OrderNumberBox)
            {
                case OrderNumberBox.NumberDecrIncr:

                    htmlDivNumberBox.Add(htmlTextBox);
                    htmlDivNumberBox.Add(htmlButtonDecr);
                    htmlDivNumberBox.Add(htmlButtonIncr);

                    break;

                case OrderNumberBox.NumberIncrDecr:

                    htmlDivNumberBox.Add(htmlTextBox);
                    htmlDivNumberBox.Add(htmlButtonIncr);
                    htmlDivNumberBox.Add(htmlButtonDecr);

                    break;

                case OrderNumberBox.DecrNumberIncr:

                    htmlDivNumberBox.Add(htmlButtonDecr);
                    htmlDivNumberBox.Add(htmlTextBox);
                    htmlDivNumberBox.Add(htmlButtonIncr);

                    break;

                case OrderNumberBox.IncrNumberDecr:

                    htmlDivNumberBox.Add(htmlButtonIncr);
                    htmlDivNumberBox.Add(htmlTextBox);
                    htmlDivNumberBox.Add(htmlButtonDecr);

                    break;

                case OrderNumberBox.DecrIncrNumber:

                    htmlDivNumberBox.Add(htmlButtonDecr);
                    htmlDivNumberBox.Add(htmlButtonIncr);
                    htmlDivNumberBox.Add(htmlTextBox);

                    break;

                case OrderNumberBox.IncrDecrNumber:

                    htmlDivNumberBox.Add(htmlButtonIncr);
                    htmlDivNumberBox.Add(htmlButtonDecr);
                    htmlDivNumberBox.Add(htmlTextBox);

                    break;

                default:
                case OrderNumberBox.NotSet:

                    break;
            }

            return htmlDivNumberBox;
        }
    }
}
