﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserControls.CtrlForm2.FormElements;
using UserControls.CtrlForm2.FormElements.FormItems.FormItemsInput;
using UserControls.CtrlForm2.HtmlElements.HtmlGroups;
using UserControls.CtrlForm2.HtmlElements.HtmlItems;

namespace UserControls.CtrlForm2.Visitors
{
    public partial class Form2HtmlVisitor
    {
        public virtual void Visit(FormCheckBox formCheckBox, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formCheckBox.BaseId);
            htmlDiv.Class.Add("form-checkbox");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formCheckBox.FormId));

            bool isRequired = formCheckBox.IsRequired ?? false;

            if (!IsPostBack)
            {
                htmlDiv.Class.Add(isRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (formCheckBox.IsEntered)
                    htmlDiv.Class.Add("form-valid");
                else
                    htmlDiv.Class.Add(isRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formCheckBox.IsHidden;
            htmlContainer.Add(htmlDiv);

            HtmlCheckBox htmlCheckBox = new HtmlCheckBox(formCheckBox.BaseId);
            htmlCheckBox.Hidden.Value = formCheckBox.IsHidden;
            htmlCheckBox.ReadOnly.Value = formCheckBox.IsReadOnly;
            htmlCheckBox.Checked.Value = formCheckBox.IsChecked;
            
            HtmlLabel htmlLabel = new HtmlLabel(formCheckBox.BaseId);
            htmlLabel.Hidden.Value = formCheckBox.IsHidden;
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

            if (!IsPostBack)
                return;

            if (!isRequired || formCheckBox.IsEntered)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formCheckBox.BaseId, "Message"));
            htmlLabelMessage.Hidden.Value = formCheckBox.IsHidden;
            htmlLabelMessage.For.Value = htmlCheckBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(formCheckBox.RequiredMessage));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
