using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Form2.Form.Content.Items.Input;
using Form2.Form.Interfaces;

namespace Form2.Form.Selectables
{
    public abstract class FormSelectable : IHidden, IDisabled
    {
        #region Fields

        private bool isHidden;

        private bool isDisabled;

        private bool isSelected;

        #endregion


        #region Properties

        public bool IsSelected
        {
            get
            {
                if (isHidden)
                    return false;

                if (isDisabled)
                    return false;

                return isSelected;
            }
            set
            {
                isSelected = value;
            }
        }

        #endregion


        #region Methods

        public abstract void SetContainer<S, V>(FormSelector<S, V> container) where S : FormSelectable;

        #endregion


        #region IHidden

        public bool? IsHidden
        {
            get { return isHidden; }
            set { isHidden = value ?? false; }
        }

        #endregion


        #region IDisabled

        public bool? IsDisabled
        {
            get { return isDisabled; }
            set { isDisabled = value ?? false; }
        }

        #endregion
    }
}
