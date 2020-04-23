﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.FormElements.FormItems.FormItemsInput
{
    public class FormRadioGroup : FormItemInput
    {
        #region Fields
        
        private readonly List<string> options;

        #endregion


        #region Properties

        public IEnumerable<string> Options
        {
            get { return options; }
            set
            {
                options.Clear();
                options.AddRange(value);
            }
        }

        public int Count
        {
            get { return options.Count; }
        }

        public string this[int index]
        {
            get { return options[index]; }
        }

        #endregion


        #region Constructors

        public FormRadioGroup(string baseId, string formId)
            : base(baseId, formId)
        {
            options = new List<string>();
        }

        public FormRadioGroup(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion
    }
}
