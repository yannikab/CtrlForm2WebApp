using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Enums;
using CtrlForm2.Form.Interfaces;

namespace CtrlForm2.Form.Content.Items.Input
{
    public class FormPasswordBox : FormTextBox
    {
        #region Constructors

        public FormPasswordBox(string baseId, string formId)
            : base(baseId, formId)
        {
        }

        public FormPasswordBox(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion
    }
}
