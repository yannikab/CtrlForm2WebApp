﻿using System;
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
            HtmlDiv htmlDiv = new HtmlDiv(formSelect.BaseId);
            htmlDiv.Class.Add("form-select");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formSelect.FormId));
            htmlDiv.Class.Add("form-field");

            if (initialize)
            {
                htmlDiv.Class.Add(formSelect.IsRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (!formSelect.IsRequired || formSelect.HasValue)
                    htmlDiv.Class.Add("form-valid");
                else
                    htmlDiv.Class.Add(formSelect.IsRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formSelect.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlSelect htmlSelect = formSelect.Size.HasValue ?
                new HtmlSelect(formSelect.BaseId, formSelect.Size.Value, formSelect.IsPostBack) :
                new HtmlSelect(formSelect.BaseId, formSelect.IsMultiSelect, formSelect.IsPostBack);
            htmlSelect.Disabled.Value = formSelect.IsDisabled;

            HtmlLabel htmlLabel = new HtmlLabel(formSelect.BaseId);
            htmlLabel.For.Value = htmlSelect.Id.Value;

            switch (formSelect.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    htmlLabel.Add(new HtmlText(formSelect.Label));

                    if (formSelect.IsRequired && !string.IsNullOrWhiteSpace(formSelect.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formSelect.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlSelect);

                    break;

                case OrderElements.MarkLabelInput:

                    if (formSelect.IsRequired && !string.IsNullOrWhiteSpace(formSelect.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formSelect.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formSelect.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlSelect);

                    break;

                case OrderElements.InputLabelMark:

                    htmlLabel.Add(new HtmlText(formSelect.Label));

                    if (formSelect.IsRequired && !string.IsNullOrWhiteSpace(formSelect.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formSelect.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlSelect);
                    htmlDiv.Add(htmlLabel);

                    break;

                case OrderElements.InputMarkLabel:

                    if (formSelect.IsRequired && !string.IsNullOrWhiteSpace(formSelect.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formSelect.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formSelect.Label));

                    htmlDiv.Add(htmlSelect);
                    htmlDiv.Add(htmlLabel);

                    break;

                case OrderElements.LabelInputMark:

                    htmlLabel.Add(new HtmlText(formSelect.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlSelect);

                    if (formSelect.IsRequired && !string.IsNullOrWhiteSpace(formSelect.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formSelect.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    break;

                case OrderElements.MarkInputLabel:

                    if (formSelect.IsRequired && !string.IsNullOrWhiteSpace(formSelect.RequiredMark))
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formSelect.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formSelect.Label));

                    htmlDiv.Add(htmlSelect);
                    htmlDiv.Add(htmlLabel);

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

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formSelect.BaseId, "Message"));
            htmlLabelMessage.Class.Add("form-validation-message");
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
