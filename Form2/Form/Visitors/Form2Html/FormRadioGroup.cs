using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Content.Items.Input.Selectors;
using Form2.Form.Enums;
using Form2.Form.Selectables;
using Form2.Html.Content;
using Form2.Html.Content.Elements;
using Form2.Html.Content.Elements.Containers;
using Form2.Html.Content.Elements.Input;

namespace Form2.Form.Visitors
{
    public partial class Form2HtmlVisitor
    {
        public virtual void Visit(FormRadioGroup formRadioGroup, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formRadioGroup.BaseId : "");
            htmlDiv.Class.Add("form-radiogroup");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formRadioGroup.FormId));
            htmlDiv.Class.Add("form-field");

            if (initialize)
            {
                htmlDiv.Class.Add(formRadioGroup.IsRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (!formRadioGroup.IsRequired || formRadioGroup.HasValue)
                    htmlDiv.Class.Add("form-valid");
                else
                    htmlDiv.Class.Add(formRadioGroup.IsRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formRadioGroup.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlRadioGroup htmlRadioGroup = new HtmlRadioGroup(formRadioGroup.BaseId, verbose, formRadioGroup.IsPostBack);
            htmlRadioGroup.Disabled.Value = formRadioGroup.IsDisabled;

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? formRadioGroup.BaseId : "");
            htmlLabel.Add(new HtmlText(formRadioGroup.Label));

            switch (formRadioGroup.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(Mark(formRadioGroup));
                    htmlDiv.Add(htmlRadioGroup);

                    break;

                case OrderElements.MarkLabelInput:

                    htmlDiv.Add(Mark(formRadioGroup));
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlRadioGroup);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlRadioGroup);
                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(Mark(formRadioGroup));

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlRadioGroup);
                    htmlDiv.Add(Mark(formRadioGroup));
                    htmlDiv.Add(htmlLabel);

                    break;

                case OrderElements.LabelInputMark:

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlRadioGroup);
                    htmlDiv.Add(Mark(formRadioGroup));

                    break;

                case OrderElements.MarkInputLabel:

                    htmlDiv.Add(Mark(formRadioGroup));
                    htmlDiv.Add(htmlRadioGroup);
                    htmlDiv.Add(htmlLabel);

                    break;

                default:
                case OrderElements.NotSet:

                    break;
            }

            foreach (var formRadioButton in formRadioGroup.Content)
                Visit(formRadioButton, htmlRadioGroup);

            if (initialize)
                return;

            string message = null;

            if (formRadioGroup.UseLastMessage)
            {
                if (!string.IsNullOrEmpty(formRadioGroup.LastMessage))
                    message = formRadioGroup.LastMessage;
            }
            else if (formRadioGroup.IsRequired && !formRadioGroup.HasValue)
            {
                message = formRadioGroup.RequiredMessage;
            }
            else if (!formRadioGroup.IsValid)
            {
                message = formRadioGroup.ValidationMessage;
            }

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formRadioGroup.BaseId, "Message") : "");
            htmlLabelMessage.Class.Add("form-validation-message");
            htmlLabelMessage.For.Value = htmlRadioGroup.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }

        public virtual void Visit(FormRadioButton formRadioButton, HtmlContainer htmlContainer)
        {
            HtmlRadioButton htmlRadioButton = new HtmlRadioButton(formRadioButton.FormRadioGroup.BaseId, formRadioButton.Value);
            htmlRadioButton.Hidden.Value = formRadioButton.IsHidden;
            htmlRadioButton.Disabled.Value = formRadioButton.IsDisabled;
            htmlRadioButton.Value.Value = formRadioButton.Value;
            htmlRadioButton.Checked.Value = formRadioButton.IsSelected;

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? string.Format("{0}{1}", formRadioButton.FormRadioGroup.BaseId, formRadioButton.Value) : "");
            htmlLabel.For.Value = htmlRadioButton.Id.Value;
            htmlLabel.Hidden.Value = formRadioButton.IsHidden;
            htmlLabel.Add(new HtmlText(formRadioButton.Text));

            htmlContainer.Add(htmlRadioButton);
            htmlContainer.Add(htmlLabel);
        }
    }
}
