﻿using System;
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
            HtmlDiv htmlDiv = new HtmlDiv(formCheckBox.BaseId);
            htmlDiv.Class.Add("form-item");
            htmlDiv.Class.Add("form-checkbox");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formCheckBox.FormId));

            bool isRequired = formCheckBox.IsRequired;

            if (!validate)
            {
                htmlDiv.Class.Add(isRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (formCheckBox.IsRequiredMet)
                    htmlDiv.Class.Add("form-valid");
                else
                    htmlDiv.Class.Add(isRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formCheckBox.IsHidden;
            htmlContainer.Add(htmlDiv);

            HtmlCheckBox htmlCheckBox = new HtmlCheckBox(formCheckBox.BaseId);
            htmlCheckBox.Disabled.Value = formCheckBox.IsDisabled;
            htmlCheckBox.Checked.Value = formCheckBox.Value;

            HtmlLabel htmlLabel = new HtmlLabel(formCheckBox.BaseId);
            htmlLabel.For.Value = htmlCheckBox.Id.Value;

            switch (formCheckBox.ElementOrder)
            {
                case ElementOrder.LabelMarkInput:

                    htmlLabel.Add(new HtmlText(formCheckBox.Label));

                    if (isRequired && formCheckBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formCheckBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlCheckBox);

                    break;

                case ElementOrder.MarkLabelInput:

                    if (isRequired && formCheckBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formCheckBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formCheckBox.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlCheckBox);

                    break;

                case ElementOrder.InputLabelMark:

                    htmlLabel.Add(new HtmlText(formCheckBox.Label));

                    if (isRequired && formCheckBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formCheckBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlCheckBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.InputMarkLabel:

                    if (isRequired && formCheckBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formCheckBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formCheckBox.Label));

                    htmlDiv.Add(htmlCheckBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.LabelInputMark:

                    htmlLabel.Add(new HtmlText(formCheckBox.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlCheckBox);

                    if (isRequired && formCheckBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formCheckBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    break;

                case ElementOrder.MarkInputLabel:

                    if (isRequired && formCheckBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formCheckBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formCheckBox.Label));

                    htmlDiv.Add(htmlCheckBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                default:
                case ElementOrder.NotSet:

                    break;
            }

            if (!validate)
                return;

            if (sessionState != null)
            {
                string viewStateString = (string)sessionState[formCheckBox.SessionKey];

                if (viewStateString == null)
                    formCheckBox.Content = false;

                formCheckBox.Content = viewStateString.ToLower() == "on";
            }

            if (formCheckBox.IsRequiredMet)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formCheckBox.BaseId, "Message"));
            htmlLabelMessage.Class.Add("form-validation-message"); 
            htmlLabelMessage.For.Value = htmlCheckBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(formCheckBox.RequiredMessage));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
