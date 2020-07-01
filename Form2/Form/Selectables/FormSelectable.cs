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

        private bool hidden;

        private bool disabled;

        private bool selected;

        #endregion


        #region Properties

        public bool IsSelected
        {
            get
            {
                if (hidden)
                    return false;

                if (disabled)
                    return false;

                return selected;
            }
            set
            {
                selected = value;
            }
        }

        #endregion


        #region Methods

        public abstract void SetContainer<S, V>(FormSelector<S, V> container) where S : FormSelectable;

        #endregion


        #region IHidden

        public bool? Hidden
        {
            set { hidden = value ?? false; }
        }

        public bool IsHidden
        {
            get { return hidden; }
        }

        #endregion


        #region IDisabled

        public bool? Disabled
        {
            set { disabled = value ?? false; }
        }

        public bool IsDisabled
        {
            get { return disabled; }
        }

        #endregion
    }
}
