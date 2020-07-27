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

        private readonly string name;

        private FormGroup container;

        private bool? hidden;

        #endregion


        #region Properties

        public string Name
        {
            get { return name; }
        }

        public string Path
        {
            get
            {
                if (container == null)
                    return "";

                return string.Format("{0}{1}", container.Path, name);
            }
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

        public FormContent(string name)
        {
            if (name == null)
                throw new ArgumentNullException();

            this.name = name;

            container = null;

            hidden = null;
        }

        #endregion
    }
}
