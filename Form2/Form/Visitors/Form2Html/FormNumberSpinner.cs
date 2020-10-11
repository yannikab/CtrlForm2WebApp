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
        public virtual void Visit(FormNumberSpinner formNumberSpinner, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(verbose ? formNumberSpinner.Path : "");
            htmlDiv.Class.Add("formNumberSpinner");
            if (!string.IsNullOrWhiteSpace(formNumberSpinner.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formNumberSpinner.Path));
            htmlDiv.Class.Add("formField");

            if (initialize)
            {
                htmlDiv.Class.Add(formNumberSpinner.IsRequired ? "formRequired" : "formOptional");
            }
            else
            {
                if (formNumberSpinner.HasValue)
                    htmlDiv.Class.Add(formNumberSpinner.IsValid ? "formValid" : "formInvalid");
                else
                    htmlDiv.Class.Add(formNumberSpinner.IsRequired ? "formNotEntered" : "formOptional");
            }

            htmlDiv.Hidden.Value = formNumberSpinner.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlTextBox htmlTextBox = new HtmlTextBox(formNumberSpinner.Path);
            htmlTextBox.Disabled.Value = formNumberSpinner.IsDisabled;
            htmlTextBox.ReadOnly.Value = formNumberSpinner.IsReadOnly || !formNumberSpinner.IsDirectInput;
            htmlTextBox.Value.Value = formNumberSpinner.HasValue ? formNumberSpinner.Value.ToString(string.Format("F{0}", formNumberSpinner.Precision)) : "";

            string placeholder = null;

            if (!string.IsNullOrWhiteSpace(formNumberSpinner.Placeholder))
            {
                if (formNumberSpinner.IsRequired && formNumberSpinner.IsRequiredInPlaceholder && !string.IsNullOrWhiteSpace(formNumberSpinner.RequiredMark))
                    placeholder = string.Format("{0} {1}", formNumberSpinner.Placeholder, formNumberSpinner.RequiredMark);
                else if (!formNumberSpinner.IsRequired && formNumberSpinner.IsOptionalInPlaceholder && !string.IsNullOrWhiteSpace(formNumberSpinner.OptionalMark))
                    placeholder = string.Format("{0} {1}", formNumberSpinner.Placeholder, formNumberSpinner.OptionalMark);
                else
                    placeholder = formNumberSpinner.Placeholder;
            }

            htmlTextBox.Placeholder.Value = placeholder;

            if (formNumberSpinner.Update)
            {
                htmlTextBox.Change.Value = string.Format("__doPostBack('{0}', '');", formNumberSpinner.Path);
            }
            else
            {
                if (formNumberSpinner.HasValue)
                    htmlTextBox.DataNumber.Value = formNumberSpinner.Value;

                if (formNumberSpinner.Min.HasValue)
                    htmlTextBox.DataMin.Value = formNumberSpinner.Min.Value;

                if (formNumberSpinner.Max.HasValue)
                    htmlTextBox.DataMax.Value = formNumberSpinner.Max.Value;

                htmlTextBox.DataStep.Value = formNumberSpinner.Step;

                htmlTextBox.DataPrecision.Value = formNumberSpinner.Precision;

                htmlTextBox.Blur.Value = string.Format("NumberSpinnerBlur('{0}')", htmlTextBox.Id.Value);
                
                scriptRegistry.Include("NumberSpinnerBlur");
            }

            HtmlDiv htmlDivNumberSpinner = BuildDivNumberSpinner(formNumberSpinner, htmlTextBox);

            switch (formNumberSpinner.OrderElements)
            {
                case OrderElements.LabelMarkInput:

                    AddLabelMark(formNumberSpinner, htmlTextBox, htmlDiv);
                    htmlDiv.Add(htmlDivNumberSpinner);

                    break;

                case OrderElements.MarkLabelInput:

                    AddMarkLabel(formNumberSpinner, htmlTextBox, htmlDiv);
                    htmlDiv.Add(htmlDivNumberSpinner);

                    break;

                case OrderElements.InputLabelMark:

                    htmlDiv.Add(htmlTextBox);
                    AddLabelMark(formNumberSpinner, htmlTextBox, htmlDiv);

                    break;

                case OrderElements.InputMarkLabel:

                    htmlDiv.Add(htmlDivNumberSpinner);
                    AddMarkLabel(formNumberSpinner, htmlTextBox, htmlDiv);

                    break;

                case OrderElements.LabelInputMark:

                    AddLabel(formNumberSpinner, htmlTextBox, htmlDiv);
                    htmlDiv.Add(htmlDivNumberSpinner);
                    AddMark(formNumberSpinner, htmlTextBox, htmlDiv);

                    break;

                case OrderElements.MarkInputLabel:

                    AddMark(formNumberSpinner, htmlTextBox, htmlDiv);
                    htmlDiv.Add(htmlDivNumberSpinner);
                    AddLabel(formNumberSpinner, htmlTextBox, htmlDiv);

                    break;

                default:
                case OrderElements.NotSet:

                    break;
            }

            if (initialize)
                return;

            string message = null;

            if (formNumberSpinner.UseLastMessage)
            {
                if (!string.IsNullOrEmpty(formNumberSpinner.LastMessage))
                    message = formNumberSpinner.LastMessage;
            }
            else if (formNumberSpinner.IsRequired && !formNumberSpinner.HasValue)
            {
                message = formNumberSpinner.RequiredMessage;
            }
            else if (!formNumberSpinner.IsValid)
            {
                message = formNumberSpinner.ValidationMessage;
            }

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(verbose ? string.Format("{0}{1}", formNumberSpinner.Path, "Message") : "");
            htmlLabelMessage.Class.Add("formValidationMessage");
            htmlLabelMessage.For.Value = htmlTextBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }

        private HtmlDiv BuildDivNumberSpinner(FormNumberSpinner formNumberSpinner, HtmlTextBox htmlTextBox)
        {
            HtmlDiv htmlDivNumberSpinner = new HtmlDiv("");

            string btnDecrName = verbose ? string.Format("{0}{1}", "Decr", formNumberSpinner.Path) : "";
            string btnIncrName = verbose ? string.Format("{0}{1}", "Incr", formNumberSpinner.Path) : "";

            string btnDecrOnClick = null;
            string btnIncrOnClick = null;

            if (formNumberSpinner.Update)
            {
                btnDecrOnClick = string.Format("__doPostBack('{0}', 'Decr');", formNumberSpinner.Path);
                btnIncrOnClick = string.Format("__doPostBack('{0}', 'Incr');", formNumberSpinner.Path);
            }
            else if (htmlTextBox.Id.IsSet)
            {
                btnDecrOnClick = string.Format("NumberSpinnerDecr('{0}');", htmlTextBox.Id.Value);
                btnIncrOnClick = string.Format("NumberSpinnerIncr('{0}');", htmlTextBox.Id.Value);

                scriptRegistry.Include("NumberSpinnerDecr");
                scriptRegistry.Include("NumberSpinnerIncr");
            }

            HtmlButton htmlButtonDecr = new HtmlButton(btnDecrName, btnDecrOnClick);
            HtmlButton htmlButtonIncr = new HtmlButton(btnIncrName, btnIncrOnClick);

            htmlButtonDecr.Value.Value = formNumberSpinner.DecrText;
            htmlButtonIncr.Value.Value = formNumberSpinner.IncrText;

            htmlButtonDecr.Disabled.Value = htmlButtonIncr.Disabled.Value = formNumberSpinner.IsReadOnly || (formNumberSpinner.Update && !formNumberSpinner.HasValue);

            switch (formNumberSpinner.OrderNumberSpinner)
            {
                case OrderNumberSpinner.NumberDecrIncr:

                    htmlDivNumberSpinner.Add(htmlTextBox);
                    htmlDivNumberSpinner.Add(htmlButtonDecr);
                    htmlDivNumberSpinner.Add(htmlButtonIncr);

                    break;

                case OrderNumberSpinner.NumberIncrDecr:

                    htmlDivNumberSpinner.Add(htmlTextBox);
                    htmlDivNumberSpinner.Add(htmlButtonIncr);
                    htmlDivNumberSpinner.Add(htmlButtonDecr);

                    break;

                case OrderNumberSpinner.DecrNumberIncr:

                    htmlDivNumberSpinner.Add(htmlButtonDecr);
                    htmlDivNumberSpinner.Add(htmlTextBox);
                    htmlDivNumberSpinner.Add(htmlButtonIncr);

                    break;

                case OrderNumberSpinner.IncrNumberDecr:

                    htmlDivNumberSpinner.Add(htmlButtonIncr);
                    htmlDivNumberSpinner.Add(htmlTextBox);
                    htmlDivNumberSpinner.Add(htmlButtonDecr);

                    break;

                case OrderNumberSpinner.DecrIncrNumber:

                    htmlDivNumberSpinner.Add(htmlButtonDecr);
                    htmlDivNumberSpinner.Add(htmlButtonIncr);
                    htmlDivNumberSpinner.Add(htmlTextBox);

                    break;

                case OrderNumberSpinner.IncrDecrNumber:

                    htmlDivNumberSpinner.Add(htmlButtonIncr);
                    htmlDivNumberSpinner.Add(htmlButtonDecr);
                    htmlDivNumberSpinner.Add(htmlTextBox);

                    break;

                default:
                case OrderNumberSpinner.NotSet:

                    break;
            }

            return htmlDivNumberSpinner;
        }


        #region ScriptRegistry

        [SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]

        private partial class ScriptRegistry
        {
            private static string NumberSpinnerDecr
            {
                get
                {
                    return @"
                        function NumberSpinnerDecr(id) {

                            let textBox = document.getElementById(id);

                            if (textBox == null)
                                return;

                            if (typeof textBox.dataset.number === 'undefined')
                                return;

                            if (typeof textBox.dataset.step === 'undefined')
                                return;

                            if (typeof textBox.dataset.precision === 'undefined')
                                return;

                            let number = Number(textBox.dataset.number);
                            let step = Number(textBox.dataset.step)
                            let precision = Number(textBox.dataset.precision)

                            if (isNaN(number) || isNaN(step) || isNaN(precision))
                                return;

                            if (typeof textBox.dataset.min === 'undefined')
                            {
                                textBox.dataset.number = number - step;
                            }
                            else
                            {
                                let min = Number(textBox.dataset.min);

                                if (isNaN(min))
                                    return;

                                textBox.dataset.number = number - step < min ? min : number - step;
                            }

                            textBox.value = (Math.round(textBox.dataset.number * Math.pow(10, precision)) / Math.pow(10, precision)).toFixed(precision);
                        }
                    ";
                }
            }

            private static string NumberSpinnerIncr
            {
                get
                {
                    return @"
                        function NumberSpinnerIncr(id) {

                            let textBox = document.getElementById(id);

                            if (textBox == null)
                                return;

                            if (typeof textBox.dataset.number === 'undefined')
                                return;

                            if (typeof textBox.dataset.step === 'undefined')
                                return;

                            if (typeof textBox.dataset.precision === 'undefined')
                                return;

                            let number = Number(textBox.dataset.number);
                            let step = Number(textBox.dataset.step)
                            let precision = Number(textBox.dataset.precision)

                            if (isNaN(number) || isNaN(step) || isNaN(precision))
                                return;

                            if (typeof textBox.dataset.max === 'undefined')
                            {
                                textBox.dataset.number = number + step;
                            }
                            else
                            {
                                let max = Number(textBox.dataset.max);

                                if (isNaN(max))
                                    return;

                                textBox.dataset.number = number + step > max ? max : number + step;
                            }

                            textBox.value = (Math.round(textBox.dataset.number * Math.pow(10, precision)) / Math.pow(10, precision)).toFixed(precision);
                        }
                    ";
                }
            }

            private static string NumberSpinnerBlur
            {
                get
                {
                    return @"
                        function NumberSpinnerBlur(id) {

                            let textBox = document.getElementById(id);

                            if (textBox == null)
                                return;

                            if (typeof textBox.dataset.number === 'undefined')
                                return;

                            if (typeof textBox.dataset.precision === 'undefined')
                                return;

                            let number = Number(textBox.dataset.number);
                            let precision = Number(textBox.dataset.precision)
                            
                            if (isNaN(number) || isNaN(precision))
                                return;

                            let newNumber = Number(textBox.value);

                            if (!isNaN(newNumber))
                            {
                                textBox.dataset.number = textBox.value;
                            }

                            if (!(typeof textBox.dataset.min === 'undefined'))
                            {
                                let min = Number(textBox.dataset.min);
                        
                                if (!isNaN(min) && textBox.dataset.number < min)
                                    textBox.dataset.number = min;
                            } 
                            
                            if (!(typeof textBox.dataset.max === 'undefined'))
                            {
                                let max = Number(textBox.dataset.max);
                        
                                if (!isNaN(max) && textBox.dataset.number > max)
                                    textBox.dataset.number = max;
                            }

                            textBox.value = (Math.round(textBox.dataset.number * Math.pow(10, precision)) / Math.pow(10, precision)).toFixed(precision);
                        }
                    ";
                }
            }
        }

        #endregion
    }
}
