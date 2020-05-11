using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Selectables;

namespace CtrlForm2.Form.Content.Items.Input
{
    public abstract class FormSelector<T> : FormInput where T : FormSelectable
    {
        #region Fields

        private readonly List<T> options;

        #endregion


        #region Properties

        public virtual IEnumerable<T> Options
        {
            get { return options; }
            set
            {
                //if (!IsMultiSelect && value.Count(o => o.IsSelected) > 1)
                //    throw new ArgumentException();

                int count = options.Count;

                for (int i = 0; i < count; i++)
                    Remove(options[0]);

                foreach (var o in value)
                    Add(o);
            }
        }

        public abstract bool IsMultiSelect
        {
            get;
        }

        #endregion


        #region Methods

        public void Add(T option)
        {
            Insert(options.Count, option);
        }

        public void Insert(int index, T option)
        {
            if (options.Contains(option))
                return;

            //if (!IsMultiSelect && option.IsSelected && options.Any(o => o.IsSelected))
            //    throw new ArgumentException();

            if (!IsMultiSelect && option.IsSelected && options.Any(o => o.IsSelected))
            {
                foreach (T o in options)
                    o.IsSelected = false;
            }

            options.Insert(index, option);

            option.SetContainer(this);
        }

        public virtual bool Remove(T option)
        {
            if (!options.Contains(option))
                return false;

            bool wasRemoved = options.Remove(option);

            if (wasRemoved)
                option.SetContainer<T>(null);

            return wasRemoved;
        }

        #endregion


        #region Constructors

        public FormSelector(string baseId, string formId)
            : base(baseId, formId)
        {
            options = new List<T>();
        }

        public FormSelector(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion
    }
}
