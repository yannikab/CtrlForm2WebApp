using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.Interfaces;

namespace UserControls.CtrlForm2.FormElements.FormItems
{
    public abstract class FormItem : IContainable
    {
        #region Fields

        private IContainer container;

        private readonly string baseId;

        private readonly string formId;

        private bool isHidden;

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

        public bool IsHidden
        {
            get { return isHidden; }
            set { isHidden = value; }
        }

        public int Depth
        {
            get { return (container as FormGroup) == null ? 0 : (container as FormGroup).Depth + 1; }
        }

        #endregion


        #region IContainable

        public IContainer Container
        {
            get { return container; }
            set
            {
                if (value != null && value as FormGroup == null)
                    throw new ArgumentException();

                container = value;
            }
        }

        #endregion


        #region Constructors

        public FormItem(string baseId, string formId)
        {
            if (string.IsNullOrEmpty(baseId))
                throw new ArgumentException();

            if (string.IsNullOrEmpty(formId))
                throw new ArgumentException();

            this.baseId = baseId;
            this.formId = formId;
        }

        #endregion
    }
}
