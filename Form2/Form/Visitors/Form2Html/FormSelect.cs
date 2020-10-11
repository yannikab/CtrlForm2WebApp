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

namespace Form2.Form.Visitors
{
    public partial class Form2HtmlVisitor
    {
        public virtual void Visit(FormSelect formSelect, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formSelect.Path : "");
            htmlDiv.Class.Add("formSelect");
            if (!string.IsNullOrWhiteSpace(formSelect.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formSelect.Path));
            htmlDiv.Class.Add("formField");

            if (initialize)
            {
                htmlDiv.Class.Add(formSelect.IsRequired ? "formRequired" : "formOptional");
            }
            else
            {
                if (!formSelect.IsRequired || formSelect.HasValue)
                    htmlDiv.Class.Add("formValid");
                else
                    htmlDiv.Class.Add(formSelect.IsRequired ? "formNotEntered" : "formOptional");
            }

            htmlDiv.Hidden.Value = formSelect.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlSelect htmlSelect = formSelect.Size.HasValue ?
                new HtmlSelect(formSelect.Path, formSelect.Size.Value, formSelect.Update) :
                new HtmlSelect(formSelect.Path, formSelect.IsMultiSelect, formSelect.Update);
            htmlSelect.Disabled.Value = formSelect.IsDisabled;

            switch (formSelect.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    AddLabelMark(formSelect, htmlSelect, htmlDiv);
                    htmlDiv.Add(htmlSelect);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMarkLabel(formSelect, htmlSelect, htmlDiv);
                    htmlDiv.Add(htmlSelect);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlSelect);
                    AddLabelMark(formSelect, htmlSelect, htmlDiv);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlSelect);
                    AddMarkLabel(formSelect, htmlSelect, htmlDiv);

                    break;

                case OrderElements.LabelInputMark:

                    AddLabel(formSelect, htmlSelect, htmlDiv);
                    htmlDiv.Add(htmlSelect);
                    AddMark(formSelect, htmlSelect, htmlDiv);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formSelect, htmlSelect, htmlDiv);
                    htmlDiv.Add(htmlSelect);
                    AddLabel(formSelect, htmlSelect, htmlDiv);

                    break;

                default:
                case OrderElements.NotSet:

                    break;
            }

            foreach (var formOption in formSelect.Content)
                Visit(formOption, htmlSelect);

            if (initialize)
                return;

            string message = null;

            if (formSelect.UseLastMessage)
            {
                if (!string.IsNullOrEmpty(formSelect.LastMessage))
                    message = formSelect.LastMessage;
            }
            else if (formSelect.IsRequired && !formSelect.HasValue)
            {
                message = formSelect.RequiredMessage;
            }
            else if (!formSelect.IsValid)
            {
                message = formSelect.ValidationMessage;
            }

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formSelect.Path, "Message") : "");
            htmlLabelMessage.Class.Add("formValidationMessage");
            htmlLabelMessage.For.Value = htmlSelect.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }

        public virtual void Visit(FormOption formOption, HtmlContainer htmlContainer)
        {
            HtmlOption htmlOption = new HtmlOption(formOption.Value);
            htmlOption.Add(new HtmlText(formOption.Text));
            htmlOption.Hidden.Value = formOption.IsHidden;
            htmlOption.Disabled.Value = formOption.IsDisabled;
            htmlOption.Selected.Value = formOption.IsSelected;

            htmlContainer.Add(htmlOption);
        }
    }
}
