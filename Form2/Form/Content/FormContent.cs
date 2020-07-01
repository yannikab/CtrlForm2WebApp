using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Form2.Form.Enums;
using Form2.Form.Interfaces;

namespace Form2.Form.Content
{
    [SuppressMessage("Style", "IDE0016:Use 'throw' expression", Justification = "<Pending>")]

    public abstract class FormContent : IHidden
    {
        #region Fields

        private readonly string baseId;

        private readonly string formId;

        private FormGroup container;

        private bool? hidden;

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

        public string SessionKey
        {
            get
            {
                return string.Format("{0}_{1}", GetType().Name, BaseId);
            }
        }

        #endregion


        #region IHidden

        public bool? Hidden
        {
            set { hidden = value; }
        }

        public bool IsHidden
        {
            get
            {
                if (hidden.HasValue)
                    return hidden.Value;

                if (container == null)
                    return false;

                return container.IsHidden;
            }
        }

        #endregion


        #region Constructors

        public FormContent(string baseId, string formId)
        {
            if (baseId == null)
                throw new ArgumentNullException();

            if (formId == null)
                throw new ArgumentNullException();

            this.baseId = baseId;
            this.formId = formId;

            container = null;

            hidden = null;
        }

        #endregion
    }
}
