﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Content.Items.Input.Selectors;
using CtrlForm2.Form.Enums;
using CtrlForm2.Form.Selectables;
using CtrlForm2.Html.Content;
using CtrlForm2.Html.Content.Elements;
using CtrlForm2.Html.Content.Elements.Containers;
using CtrlForm2.Html.Content.Elements.Input;

namespace CtrlForm2.Form.Visitors
{
    public partial class Form2HtmlVisitor
    {
        public virtual void Visit(FormRadioGroup formRadioGroup, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formRadioGroup.BaseId);
            htmlDiv.Class.Add("form-radiogroup");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formRadioGroup.FormId));

            bool isRequired = formRadioGroup.IsRequired ?? false;

            if (!IsPostBack)
            {
                htmlDiv.Class.Add(isRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (formRadioGroup.IsRequiredMet)
                    htmlDiv.Class.Add("form-valid");
                else
                    htmlDiv.Class.Add(isRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formRadioGroup.IsHidden;
            htmlContainer.Add(htmlDiv);

            HtmlRadioGroup htmlRadioGroup = new HtmlRadioGroup(formRadioGroup.BaseId);
            htmlRadioGroup.Class.Add("form-radiogroup");
            htmlRadioGroup.Class.Add(string.Format("{0}-{1}", "form-id", formRadioGroup.FormId));
            htmlRadioGroup.Disabled.Value = formRadioGroup.IsDisabled;

            HtmlLabel htmlLabel = new HtmlLabel(formRadioGroup.BaseId);
            htmlLabel.For.Value = htmlRadioGroup.Id.Value;

            switch (formRadioGroup.ElementOrder)
            {
                case ElementOrder.LabelMarkInput:

                    htmlLabel.Add(new HtmlText(formRadioGroup.Label));

                    if (isRequired && formRadioGroup.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formRadioGroup.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlRadioGroup);

                    break;

                case ElementOrder.MarkLabelInput:

                    if (isRequired && formRadioGroup.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formRadioGroup.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formRadioGroup.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlRadioGroup);

                    break;

                case ElementOrder.InputLabelMark:

                    htmlLabel.Add(new HtmlText(formRadioGroup.Label));

                    if (isRequired && formRadioGroup.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formRadioGroup.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlRadioGroup);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.InputMarkLabel:

                    if (isRequired && formRadioGroup.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formRadioGroup.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formRadioGroup.Label));

                    htmlDiv.Add(htmlRadioGroup);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.LabelInputMark:

                    htmlLabel.Add(new HtmlText(formRadioGroup.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlRadioGroup);

                    if (isRequired && formRadioGroup.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formRadioGroup.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    break;

                case ElementOrder.MarkInputLabel:

                    if (isRequired && formRadioGroup.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formRadioGroup.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formRadioGroup.Label));

                    htmlDiv.Add(htmlRadioGroup);
                    htmlDiv.Add(htmlLabel);

                    break;

                default:
                case ElementOrder.NotSet:

                    break;
            }

            foreach (var formOption in formRadioGroup.Content)
                Visit(formOption, htmlRadioGroup);

            if (!IsPostBack)
                return;

            string message = null;

            if (!formRadioGroup.IsRequiredMet)
                message = formRadioGroup.RequiredMessage;
            else if (!formRadioGroup.IsValid)
                message = formRadioGroup.ValidationMessage;

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formRadioGroup.BaseId, "Message"));
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

            HtmlLabel htmlLabel = new HtmlLabel(string.Format("{0}{1}", formRadioButton.FormRadioGroup.BaseId, formRadioButton.Value));
            htmlLabel.For.Value = htmlRadioButton.Id.Value;
            htmlLabel.Hidden.Value = formRadioButton.IsHidden;
            htmlLabel.Add(new HtmlText(formRadioButton.Text));

            htmlContainer.Add(htmlRadioButton);
            htmlContainer.Add(htmlLabel);
        }
    }
}
