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
    public partial class Form2HtmlMELOVisitor
    {
        public virtual void Visit(FormCheckBox formCheckBox, HtmlContainer htmlContainer)
        {
            HtmlFieldset htmlFieldset = verbose ? new HtmlFieldset(formCheckBox.Path) : new HtmlFieldset();

            htmlFieldset.Class.Add("formCheckBox");

            if (!string.IsNullOrWhiteSpace(formCheckBox.CssClass))
                htmlFieldset.Class.AddRange(formCheckBox.CssClass.Split(' ').Where(s => s!= string.Empty));

            htmlFieldset.Class.AddRange(new string[] { "form-group", "switchBlock", "d-flex", "justify-content-between" });

            if (!string.IsNullOrWhiteSpace(formCheckBox.Path))
                htmlFieldset.Class.Add(string.Format("{0}{1}", "formId", formCheckBox.Path));

            htmlFieldset.Class.Add("formField");

            if (initialize)
            {
                htmlFieldset.Class.Add(formCheckBox.IsRequired ? "formRequired" : "formOptional");
            }
            else
            {
                if (!formCheckBox.IsRequired || formCheckBox.Value)
                    htmlFieldset.Class.Add("formValid");
                else
                    htmlFieldset.Class.Add(formCheckBox.IsRequired ? "formNotEntered" : "formOptional");
            }

            htmlFieldset.Hidden.Value = formCheckBox.IsHidden;

            htmlContainer.Add(htmlFieldset);

            HtmlLabel htmlLabel = CreateCheckBox(formCheckBox);

            HtmlCheckBox htmlCheckBox = htmlLabel.Contents.Single(c => c is HtmlCheckBox) as HtmlCheckBox;

            if (!initialize && firstInvalidId == null)
                if (formCheckBox.IsRequired && !formCheckBox.HasValue || !formCheckBox.IsValid)
                    firstInvalidId = htmlCheckBox.Id.Value;

            switch (formCheckBox.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    AddLabelMark(formCheckBox, htmlCheckBox, htmlFieldset);
                    htmlFieldset.Add(htmlLabel);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMarkLabel(formCheckBox, htmlCheckBox, htmlFieldset);
                    htmlFieldset.Add(htmlLabel);

                    break;

                case OrderElements.InputLabelMark:

                    htmlFieldset.Add(htmlLabel);
                    AddLabelMark(formCheckBox, htmlCheckBox, htmlFieldset);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlFieldset.Add(htmlLabel);
                    AddMarkLabel(formCheckBox, htmlCheckBox, htmlFieldset);

                    break;

                case OrderElements.LabelInputMark:

                    AddLabel(formCheckBox, htmlCheckBox, htmlFieldset);
                    htmlFieldset.Add(htmlLabel);
                    AddMark(formCheckBox, htmlCheckBox, htmlFieldset);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formCheckBox, htmlCheckBox, htmlFieldset);
                    htmlFieldset.Add(htmlLabel);
                    AddLabel(formCheckBox, htmlCheckBox, htmlFieldset);

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

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formCheckBox.Path, "Message") : "");
            htmlLabelMessage.Class.Add("formValidationMessage");
            htmlLabelMessage.For.Value = htmlCheckBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(formCheckBox.RequiredMessage));
            htmlFieldset.Add(htmlLabelMessage);
        }

        private HtmlLabel CreateCheckBox(FormCheckBox formCheckBox)
        {
            HtmlLabel htmlLabel = new HtmlLabel();
            htmlLabel.Class.AddRange(new string[] { "switch", "switch-default", "switch-pill", "switch-primary", "ml-2" });

            HtmlCheckBox htmlCheckBox = new HtmlCheckBox(formCheckBox.Path);
            htmlCheckBox.Class.Add("switch-input");
            htmlCheckBox.Disabled.Value = formCheckBox.IsDisabled;
            htmlCheckBox.Checked.Value = formCheckBox.Value;
            htmlLabel.Add(htmlCheckBox);

            HtmlSpan htmlSpan = new HtmlSpan();
            htmlSpan.Class.Add("switch-label");
            htmlLabel.Add(htmlSpan);

            htmlSpan = new HtmlSpan();
            htmlSpan.Class.Add("switch-handle");
            htmlLabel.Add(htmlSpan);

            return htmlLabel;
        }
    }
}
