using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Enums;
using Form2.Form.Interfaces;

namespace Form2.Form.Content.Items.Input
{
    [SuppressMessage("Style", "IDE0016:Use 'throw' expression", Justification = "<Pending>")]
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]

    public class FormNumberBox : FormInput<string, decimal>, IReadOnly, IValidate<decimal>, IPostBack
    {
        #region Fields

        private string placeholder;

        private OrderNumberBox orderNumberBox;

        private FormIcon icon;

        private string decrText;

        private string incrText;

        private decimal? min;

        private decimal? max;

        private decimal step;

        private bool? readOnly;

        private bool? directInput;

        private Func<decimal, string> validator;

        private Action<decimal> actionInvalid;

        #endregion


        #region Properties

        public string Placeholder
        {
            get { return placeholder; }
            set { placeholder = value; }
        }

        public OrderNumberBox OrderNumberBox
        {
            get { return orderNumberBox; }
            set { orderNumberBox = value; }
        }

        public FormIcon Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public string DecrText
        {
            get { return decrText; }
            set { decrText = value; }
        }

        public string IncrText
        {
            get { return incrText; }
            set { incrText = value; }
        }

        public decimal? Min
        {
            get { return min; }
            set
            {
                if (max.HasValue && value > max.Value)
                    throw new ArgumentException();

                if (Value < value)
                    Content = value.ToString();

                min = value;
            }
        }

        public decimal? Max
        {
            get { return max; }
            set
            {
                if (min.HasValue && value < min.Value)
                    throw new ArgumentException();

                if (Value > value)
                    Content = value.ToString();

                max = value;
            }
        }

        public decimal Step
        {
            get { return step; }
            set { step = value; }
        }

        public bool? DirectInput
        {
            set { directInput = value; }
        }

        public bool IsDirectInput
        {
            get
            {
                if (IsReadOnly)
                    return false;

                if (directInput.HasValue)
                    return directInput.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return false;

                return container.IsDirectInput;
            }
        }

        public override string Content
        {
            get { return base.Content; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                if (!decimal.TryParse(value, out decimal val))
                {
                    base.Content = "";
                    return;
                }

                if (min.HasValue && val < min.Value)
                    value = min.Value.ToString();

                if (max.HasValue && val > max.Value)
                    value = max.Value.ToString();

                base.Content = value;
            }
        }

        public override decimal Value
        {
            get { try { return decimal.Parse(Content); } catch { return default; }; }
        }

        public override bool HasValue
        {
            get { try { decimal.Parse(Content); return true; } catch { return false; } }
        }

        #endregion


        #region IRequired

        public override bool? Required
        {
            set { base.Required = value; }
        }

        public override bool IsRequired
        {
            get
            {
                if (IsReadOnly)
                    return false;

                return base.IsRequired;
            }
        }

        #endregion


        #region IReadOnly

        public bool? ReadOnly
        {
            set { readOnly = value; }
        }

        public bool IsReadOnly
        {
            get
            {
                // a user can not be expected to fill out an input element that is hidden
                if (IsHidden)
                    return false;

                // a user can not be expected to fill out an input element that is disabled
                if (IsDisabled)
                    return false;

                if (readOnly.HasValue)
                    return readOnly.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return false;

                return container.IsReadOnly;
            }
        }

        #endregion


        #region IValidate<decimal>

        public Func<decimal, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<decimal> ActionInvalid
        {
            get { return actionInvalid; }
            set { actionInvalid = value; }
        }

        public string ValidationMessage
        {
            get { return Validator(Value); }
        }

        public bool IsValid
        {
            get
            {
                // a user can not edit hidden elements, it is unfair for them to participate in validation
                if (IsHidden)
                    return true;

                // disabled elements are not submitted, it does not make sense to validate them
                if (IsDisabled)
                    return true;

                // a user can not edit readonly elements, it is unfair for them to participate in validation
                if (IsReadOnly)
                    return true;

                return HasValue ? ValidationMessage == null : !IsRequired;
            }
        }

        #endregion


        #region IPostBack

        public bool IsPostBack
        {
            get { return true; }
            set { return; }
        }

        #endregion


        #region Constructors

        public FormNumberBox(string name)
            : base(name)
        {
            Content = "";
            placeholder = "";
            min = null;
            max = null;
            step = 1;

            orderNumberBox = OrderNumberBox.NumberDecrIncr;
            icon = FormIcon.NotSet;
            decrText = "▼";
            incrText = "▲";

            readOnly = null;

            validator = (v) => { return null; };
            actionInvalid = (v) => { return; };
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0} (Name: '{1}', Label: '{2}', Value: '{3}')", GetType().Name, Name, Label, Value);
        }

        #endregion
    }
}
