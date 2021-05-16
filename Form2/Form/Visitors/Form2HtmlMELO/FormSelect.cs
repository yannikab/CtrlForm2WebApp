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
    public partial class Form2HtmlMELOVisitor
    {
        public virtual void Visit(FormSelect formSelect, HtmlContainer htmlContainer)
        {
            HtmlFieldset htmlFieldset = verbose ? new HtmlFieldset(formSelect.Path) : new HtmlFieldset();

            htmlFieldset.Class.Add("formSelect");

            if (!string.IsNullOrWhiteSpace(formSelect.CssClass))
                htmlFieldset.Class.AddRange(formSelect.CssClass.Split(' ').Where(s => s != string.Empty));

            htmlFieldset.Class.Add("form-group");

            if (!string.IsNullOrWhiteSpace(formSelect.Path))
                htmlFieldset.Class.Add(string.Format("{0}{1}", "formId", formSelect.Path));

            htmlFieldset.Class.Add("formField");

            if (initialize)
            {
                htmlFieldset.Class.Add(formSelect.IsRequired ? "formRequired" : "formOptional");
            }
            else
            {
                if (!formSelect.IsRequired || formSelect.HasValue)
                    htmlFieldset.Class.Add("formValid");
                else
                    htmlFieldset.Class.Add(formSelect.IsRequired ? "formNotEntered" : "formOptional");
            }

            htmlFieldset.Hidden.Value = formSelect.IsHidden;

            htmlContainer.Add(htmlFieldset);

            HtmlSelect htmlSelect = formSelect.Size.HasValue ?
                new HtmlSelect(formSelect.Path, formSelect.Size.Value, formSelect.Update) :
                new HtmlSelect(formSelect.Path, formSelect.IsMultiSelect, formSelect.Update);
            htmlSelect.Class.Add("form-control");
            htmlSelect.Disabled.Value = formSelect.IsDisabled;

            if (!initialize && firstInvalidId == null)
                if (formSelect.IsRequired && !formSelect.HasValue || !formSelect.IsValid)
                    firstInvalidId = htmlSelect.Id.Value;

            switch (formSelect.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    AddLabelMark(formSelect, htmlSelect, htmlFieldset);
                    htmlFieldset.Add(htmlSelect);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMarkLabel(formSelect, htmlSelect, htmlFieldset);
                    htmlFieldset.Add(htmlSelect);

                    break;

                case OrderElements.InputLabelMark:

                    htmlFieldset.Add(htmlSelect);
                    AddLabelMark(formSelect, htmlSelect, htmlFieldset);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlFieldset.Add(htmlSelect);
                    AddMarkLabel(formSelect, htmlSelect, htmlFieldset);

                    break;

                case OrderElements.LabelInputMark:

                    AddLabel(formSelect, htmlSelect, htmlFieldset);
                    htmlFieldset.Add(htmlSelect);
                    AddMark(formSelect, htmlSelect, htmlFieldset);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formSelect, htmlSelect, htmlFieldset);
                    htmlFieldset.Add(htmlSelect);
                    AddLabel(formSelect, htmlSelect, htmlFieldset);

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
            htmlFieldset.Add(htmlLabelMessage);
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
