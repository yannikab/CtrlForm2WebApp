using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using CtrlForm2.Form.Content.Items.Input;

namespace CtrlForm2.Form.Selectables
{
    public abstract class FormSelectable
    {
        #region Fields

        private bool isSelected;

        #endregion


        #region Properties

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        #endregion


        #region Methods

        public abstract void SetContainer<T>(FormSelector<T> container) where T : FormSelectable;

        #endregion
    }
}
