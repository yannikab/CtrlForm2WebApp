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
        public virtual void Visit(FormNumberSpinner formNumberSpinner, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formNumberSpinner.Path : "");
            htmlDiv.Class.Add("formNumberSpinner");
            if (!string.IsNullOrWhiteSpace(formNumberSpinner.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formNumberSpinner.Path));
            htmlDiv.Class.Add("formField");

            if (initialize)
            {
                htmlDiv.Class.Add(formNumberSpinner.IsRequired ? "formRequired" : "formOptional");
            }
            else
            {
                if (formNumberSpinner.HasValue)
                    htmlDiv.Class.Add(formNumberSpinner.IsValid ? "formValid" : "formInvalid");
                else
                    htmlDiv.Class.Add(formNumberSpinner.IsRequired ? "formNotEntered" : "formOptional");
            }

            htmlDiv.Hidden.Value = formNumberSpinner.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlTextBox htmlTextBox = new HtmlTextBox(formNumberSpinner.Path);
            htmlTextBox.Disabled.Value = formNumberSpinner.IsDisabled;
            htmlTextBox.ReadOnly.Value = formNumberSpinner.IsReadOnly || !formNumberSpinner.IsDirectInput;
            htmlTextBox.Value.Value = formNumberSpinner.HasValue ? formNumberSpinner.Value.ToString() : "";

            string placeholder = null;

            if (!string.IsNullOrWhiteSpace(formNumberSpinner.Placeholder))
            {
                if (formNumberSpinner.IsRequired && formNumberSpinner.IsRequiredInPlaceholder && !string.IsNullOrWhiteSpace(formNumberSpinner.RequiredMark))
                    placeholder = string.Format("{0} {1}", formNumberSpinner.Placeholder, formNumberSpinner.RequiredMark);
                else if (!formNumberSpinner.IsRequired && formNumberSpinner.IsOptionalInPlaceholder && !string.IsNullOrWhiteSpace(formNumberSpinner.OptionalMark))
                    placeholder = string.Format("{0} {1}", formNumberSpinner.Placeholder, formNumberSpinner.OptionalMark);
                else
                    placeholder = formNumberSpinner.Placeholder;
            }

            htmlTextBox.Placeholder.Value = placeholder;

            htmlTextBox.Change.Value = string.Format("__doPostBack('{0}', '');", formNumberSpinner.Path);

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? formNumberSpinner.Path : "");
            htmlLabel.For.Value = htmlTextBox.Id.Value;
            htmlLabel.Add(new HtmlText(formNumberSpinner.Label));

            HtmlDiv htmlDivNumberBox = BuildNumberBoxDiv(formNumberSpinner, htmlTextBox);

            switch (formNumberSpinner.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    htmlDiv.Add(htmlLabel);
                    AddMark(formNumberSpinner, htmlDiv);
                    htmlDiv.Add(htmlDivNumberBox);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMark(formNumberSpinner, htmlDiv);
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlDivNumberBox);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlDivNumberBox);
                    htmlDiv.Add(htmlLabel);
                    AddMark(formNumberSpinner, htmlDiv);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlDivNumberBox);
                    AddMark(formNumberSpinner, htmlDiv);
                    htmlDiv.Add(htmlLabel);

                    break;

                case OrderElements.LabelInputMark:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlDivNumberBox);
                    AddMark(formNumberSpinner, htmlDiv);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formNumberSpinner, htmlDiv);
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

            if (formNumberSpinner.UseLastMessage)
            {
                if (!string.IsNullOrEmpty(formNumberSpinner.LastMessage))
                    message = formNumberSpinner.LastMessage;
            }
            else if (formNumberSpinner.IsRequired && !formNumberSpinner.HasValue)
            {
                message = formNumberSpinner.RequiredMessage;
            }
            else if (!formNumberSpinner.IsValid)
            {
                message = formNumberSpinner.ValidationMessage;
            }

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formNumberSpinner.Path, "Message") : "");
            htmlLabelMessage.Class.Add("formValidationMessage");
            htmlLabelMessage.For.Value = htmlTextBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }

        private HtmlDiv BuildNumberBoxDiv(FormNumberSpinner formNumberSpinner, HtmlTextBox htmlTextBox)
        {
            HtmlDiv htmlDivNumberBox = new HtmlDiv("");

            string btnDecrName = verbose ? string.Format("{0}{1}", "Decr", formNumberSpinner.Path) : "";
            string btnIncrName = verbose ? string.Format("{0}{1}", "Incr", formNumberSpinner.Path) : "";

            string btnDecrOnClick = string.Format("__doPostBack('{0}', 'Decr');", formNumberSpinner.Path);
            string btnIncrOnClick = string.Format("__doPostBack('{0}', 'Incr');", formNumberSpinner.Path);

            HtmlButton htmlButtonDecr = new HtmlButton(btnDecrName, btnDecrOnClick);
            HtmlButton htmlButtonIncr = new HtmlButton(btnIncrName, btnIncrOnClick);

            htmlButtonDecr.Value.Value = formNumberSpinner.DecrText;
            htmlButtonIncr.Value.Value = formNumberSpinner.IncrText;

            htmlButtonDecr.Disabled.Value = htmlButtonIncr.Disabled.Value = formNumberSpinner.IsReadOnly || !formNumberSpinner.HasValue;
            
            switch (formNumberSpinner.OrderNumberSpinner)
            {
                case OrderNumberSpinner.NumberDecrIncr:

                    htmlDivNumberBox.Add(htmlTextBox);
                    htmlDivNumberBox.Add(htmlButtonDecr);
                    htmlDivNumberBox.Add(htmlButtonIncr);

                    break;

                case OrderNumberSpinner.NumberIncrDecr:

                    htmlDivNumberBox.Add(htmlTextBox);
                    htmlDivNumberBox.Add(htmlButtonIncr);
                    htmlDivNumberBox.Add(htmlButtonDecr);

                    break;

                case OrderNumberSpinner.DecrNumberIncr:

                    htmlDivNumberBox.Add(htmlButtonDecr);
                    htmlDivNumberBox.Add(htmlTextBox);
                    htmlDivNumberBox.Add(htmlButtonIncr);

                    break;

                case OrderNumberSpinner.IncrNumberDecr:

                    htmlDivNumberBox.Add(htmlButtonIncr);
                    htmlDivNumberBox.Add(htmlTextBox);
                    htmlDivNumberBox.Add(htmlButtonDecr);

                    break;

                case OrderNumberSpinner.DecrIncrNumber:

                    htmlDivNumberBox.Add(htmlButtonDecr);
                    htmlDivNumberBox.Add(htmlButtonIncr);
                    htmlDivNumberBox.Add(htmlTextBox);

                    break;

                case OrderNumberSpinner.IncrDecrNumber:

                    htmlDivNumberBox.Add(htmlButtonIncr);
                    htmlDivNumberBox.Add(htmlButtonDecr);
                    htmlDivNumberBox.Add(htmlTextBox);

                    break;

                default:
                case OrderNumberSpinner.NotSet:

                    break;
            }

            return htmlDivNumberBox;
        }
    }
}
