﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.FormElements.FormItems
{
    public class FormSubmit : FormItem
    {
        #region Fields

        private string text;

        #endregion


        #region Properties

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        #endregion


        #region Constructors

        public FormSubmit(string baseId, string formId)
            : base(baseId, formId)
        {
            text = "";
        }

        public FormSubmit(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}: {1}, Text: {2}", GetType().Name, BaseId, Text);
        }
    }
}
