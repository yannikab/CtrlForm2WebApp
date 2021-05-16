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
        public virtual void Visit(FormDateBox formDateBox, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = verbose ? new HtmlDiv(formDateBox.Path) : new HtmlDiv();

            htmlDiv.Class.Add("formDateBox");

            if (!string.IsNullOrWhiteSpace(formDateBox.CssClass))
                htmlDiv.Class.AddRange(formDateBox.CssClass.Split(' ').Where(s => s != string.Empty));

            if (!string.IsNullOrWhiteSpace(formDateBox.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formDateBox.Path));

            htmlDiv.Class.Add("formField");

            if (initialize)
            {
                htmlDiv.Class.Add(formDateBox.IsRequired ? "formRequired" : "formOptional");
            }
            else
            {
                if (!formDateBox.IsRequired || formDateBox.HasValue)
                    htmlDiv.Class.Add(formDateBox.IsValid ? "formValid" : "formInvalid");
                else
                    htmlDiv.Class.Add(formDateBox.IsRequired ? "formNotEntered" : "formOptional");
            }

            htmlDiv.Hidden.Value = formDateBox.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlDateBox htmlDateBox = new HtmlDateBox(formDateBox.Path);
            htmlDateBox.Disabled.Value = formDateBox.IsDisabled;
            htmlDateBox.ReadOnly.Value = formDateBox.IsReadOnly;
            htmlDateBox.Value.Value = formDateBox.HasValue ? formDateBox.Value.ToString("yyyy-MM-dd") : "";

            if (!initialize && firstInvalidId == null)
                if (formDateBox.IsRequired && !formDateBox.HasValue || !formDateBox.IsValid)
                    firstInvalidId = htmlDateBox.Id.Value;

            switch (formDateBox.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    AddLabelMark(formDateBox, htmlDateBox, htmlDiv);
                    htmlDiv.Add(htmlDateBox);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMarkLabel(formDateBox, htmlDateBox, htmlDiv);
                    htmlDiv.Add(htmlDateBox);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlDateBox);
                    AddLabelMark(formDateBox, htmlDateBox, htmlDiv);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlDateBox);
                    AddMarkLabel(formDateBox, htmlDateBox, htmlDiv);

                    break;

                case OrderElements.LabelInputMark:

                    AddLabel(formDateBox, htmlDateBox, htmlDiv);
                    htmlDiv.Add(htmlDateBox);
                    AddMark(formDateBox, htmlDateBox, htmlDiv);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formDateBox, htmlDateBox, htmlDiv);
                    htmlDiv.Add(htmlDateBox);
                    AddLabel(formDateBox, htmlDateBox, htmlDiv);

                    break;

                default:
                case OrderElements.NotSet:

                    break;
            }

            if (initialize)
                return;

            string message = null;

            if (formDateBox.UseLastMessage)
            {
                if (!string.IsNullOrEmpty(formDateBox.LastMessage))
                    message = formDateBox.LastMessage;
            }
            else if (formDateBox.IsRequired && !formDateBox.HasValue)
            {
                message = formDateBox.RequiredMessage;
            }
            else if (!formDateBox.IsValid)
            {
                message = formDateBox.ValidationMessage;
            }

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formDateBox.Path, "Message") : "");
            htmlLabelMessage.For.Value = htmlDateBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
