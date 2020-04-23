﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.FormElements.FormItems
{
    public class FormTitle : FormLabel
    {
        #region Constructors

        public FormTitle(string baseId, string formId)
            : base(baseId, formId)
        {
        }

        public FormTitle(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion
    }
}
