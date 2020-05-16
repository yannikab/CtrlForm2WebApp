﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Interfaces;

namespace CtrlForm2.Form.Content
{
    [SuppressMessage("Style", "IDE0016:Use 'throw' expression", Justification = "<Pending>")]

    public abstract class FormContent : IHidden
    {
        #region Fields

        private readonly string baseId;

        private readonly string formId;

        private FormGroup container;

        private bool? isHidden;

        #endregion


        #region Properties

        public string BaseId
        {
            get { return baseId; }
        }

        public string FormId
        {
            get { return formId; }
        }

        public virtual FormGroup Container
        {
            get { return container; }
            set { container = value; }
        }

        public int Depth
        {
            get { return container == null ? 0 : container.Depth + 1; }
        }

        #endregion


        #region IHidden

        public bool? IsHidden
        {
            get
            {
                if (isHidden.HasValue)
                    return isHidden.Value;

                if (container == null)
                    return null;

                return container.IsHidden;
            }
            set
            {
                isHidden = value;
            }
        }

        #endregion


        #region Constructors

        public FormContent(string baseId, string formId)
        {
            if (baseId == null)
                throw new ArgumentException();

            if (formId == null)
                throw new ArgumentException();

            this.baseId = baseId;
            this.formId = formId;

            Container = null;

            IsHidden = null;
        }

        #endregion
    }
}