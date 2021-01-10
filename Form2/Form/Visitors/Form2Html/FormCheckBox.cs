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
            HtmlDiv htmlDiv = verbose ? new HtmlDiv(formCheckBox.Path) : new HtmlDiv();
            htmlDiv.Class.Add("formCheckBox");
            if (!string.IsNullOrWhiteSpace(formCheckBox.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formCheckBox.Path));
            htmlDiv.Class.Add("formField");

            if (initialize)
            {
                htmlDiv.Class.Add(formCheckBox.IsRequired ? "formRequired" : "formOptional");
            }
            else
            {
                if (!formCheckBox.IsRequired || formCheckBox.Value)
                    htmlDiv.Class.Add("formValid");
                else
                    htmlDiv.Class.Add(formCheckBox.IsRequired ? "formNotEntered" : "formOptional");
            }

            htmlDiv.Hidden.Value = formCheckBox.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlCheckBox htmlCheckBox = new HtmlCheckBox(formCheckBox.Path);
            htmlCheckBox.Disabled.Value = formCheckBox.IsDisabled;
            htmlCheckBox.Checked.Value = formCheckBox.Value;

            switch (formCheckBox.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    AddLabelMark(formCheckBox, htmlCheckBox, htmlDiv);
                    htmlDiv.Add(htmlCheckBox);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMarkLabel(formCheckBox, htmlCheckBox, htmlDiv);
                    htmlDiv.Add(htmlCheckBox);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlCheckBox);
                    AddLabelMark(formCheckBox, htmlCheckBox, htmlDiv);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlCheckBox);
                    AddMarkLabel(formCheckBox, htmlCheckBox, htmlDiv);

                    break;

                case OrderElements.LabelInputMark:

                    AddLabel(formCheckBox, htmlCheckBox, htmlDiv);
                    htmlDiv.Add(htmlCheckBox);
                    AddMark(formCheckBox, htmlCheckBox, htmlDiv);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formCheckBox, htmlCheckBox, htmlDiv);
                    htmlDiv.Add(htmlCheckBox);
                    AddLabel(formCheckBox, htmlCheckBox, htmlDiv);

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
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
