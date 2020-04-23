using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.FormElements.FormItems;
using UserControls.CtrlForm2.Interfaces;

namespace UserControls.CtrlForm2.FormElements
{
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]

    public class FormGroup : FormItem, IContainer, IRequired
    {
        #region Fields

        private readonly List<IContainable> contents;

        private bool isRequired;

        private string requiredMark;

        private ElementOrder elementOrder;

        #endregion


        #region Properties

        public ElementOrder ElementOrder
        {
            get
            {
                if (elementOrder != ElementOrder.NotSet)
                    return elementOrder;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return ElementOrder.NotSet;

                return container.ElementOrder;
            }

            set
            {
                elementOrder = value;
            }
        }

        #endregion


        #region IRequired

        public bool IsRequired
        {
            get { return isRequired; }
            set { isRequired = value; }
        }

        public string RequiredMark
        {
            get
            {
                if (requiredMark != null)
                    return requiredMark;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return null;

                return container.RequiredMark;
            }
            set
            {
                requiredMark = value;
            }
        }

        #endregion


        #region IContainer

        public IReadOnlyList<IContainable> Contents
        {
            get { return contents; }
        }

        public void Add(IContainable c)
        {
            if (c != null && c as FormGroup == null && c as FormItem == null)
                throw new ArgumentException();

            if (contents.Contains(c))
                return;

            contents.Add(c);

            c.Container = this;
        }

        public bool Remove(IContainable c)
        {
            if (c != null && c as FormGroup == null && c as FormItem == null)
                throw new ArgumentException();

            if (!contents.Contains(c))
                return false;

            c.Container = null;

            return contents.Remove(c);
        }

        #endregion


        #region Constructors

        public FormGroup(string baseId, string formId)
            : base(baseId, formId)
        {
            contents = new List<IContainable>();

            requiredMark = null;
            elementOrder = ElementOrder.NotSet;
        }

        public FormGroup(string baseId)
            : this(baseId.Replace("-", ""), baseId.ToLower())
        {
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}: {1}, FormName: {2}", GetType().Name, BaseId, FormId);
        }
    }
}
