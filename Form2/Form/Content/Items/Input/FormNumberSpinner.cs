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
    [SuppressMessage("Style", "IDE0018:Inline variable declaration", Justification = "<Pending>")]
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]

    public class FormNumberSpinner : FormInput<string, decimal>, IReadOnly, IUpdate
    {
        #region Fields

        private string placeholder;

        private OrderNumberSpinner orderNumberSpinner;

        private string decrText;

        private string incrText;

        private decimal? min;

        private decimal? max;

        private decimal step;

        private long precision;

        private bool? readOnly;

        private bool? directInput;

        private bool isUpdate;

        #endregion


        #region Properties

        public string Placeholder
        {
            get { return placeholder; }
            set { placeholder = value; }
        }

        public OrderNumberSpinner OrderNumberSpinner
        {
            get { return orderNumberSpinner; }
            set { orderNumberSpinner = value; }
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

        public long Precision
        {
            get { return precision; }
            set { precision = value; }
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

                decimal val;

                if (!decimal.TryParse(value, out val))
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
            get { try { return decimal.Parse(Content); } catch { return decimal.MinValue; }; }
        }

        public override bool HasValue
        {
            get { return Value != decimal.MinValue; }
        }

        public override bool IsValid
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


        #region IUpdate

        public bool IsUpdate
        {
            get { return isUpdate; }
            set { isUpdate = value; }
        }

        #endregion


        #region Constructors

        public FormNumberSpinner(string name)
            : base(name)
        {
            Content = "";
            placeholder = "";

            min = null;
            max = null;
            step = 1;
            precision = 3;

            orderNumberSpinner = OrderNumberSpinner.NumberDecrIncr;
            decrText = "▼";
            incrText = "▲";

            readOnly = null;

            isUpdate = false;
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0} (Path: '{1}', Label: '{2}', Value: '{3}')", GetType().Name, Path, Label, Value);
        }

        #endregion
    }
}
