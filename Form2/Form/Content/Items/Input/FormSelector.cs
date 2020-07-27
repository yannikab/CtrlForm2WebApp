using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Interfaces;
using Form2.Form.Selectables;

namespace Form2.Form.Content.Items.Input
{
    public abstract class FormSelector<S, V> : FormInput<IEnumerable<S>, V>, IPostBack where S : FormSelectable
    {
        #region Fields

        private readonly List<S> selectables;

        private bool isPostBack;

        #endregion


        #region Properties

        public override IEnumerable<S> Content
        {
            get { return selectables; }
            set
            {
                //if (!IsMultiSelect && value.Count(o => o.IsSelected) > 1)
                //    throw new ArgumentException();

                int count = selectables.Count;

                for (int i = 0; i < count; i++)
                    Remove(selectables[0]);

                foreach (var selectable in value)
                    Add(selectable);
            }
        }

        public abstract bool IsMultiSelect
        {
            get;
        }

        #endregion


        #region Methods

        public void Add(S selectable)
        {
            Insert(selectables.Count, selectable);
        }

        public void Insert(int index, S selectable)
        {
            if (selectables.Contains(selectable))
                return;

            //if (!IsMultiSelect && option.IsSelected && content.Any(o => o.IsSelected))
            //    throw new ArgumentException();

            if (!IsMultiSelect && selectable.IsSelected && selectables.Any(s => s.IsSelected))
            {
                foreach (S s in selectables)
                    s.IsSelected = false;
            }

            selectables.Insert(index, selectable);

            selectable.SetContainer<S, V>(this);
        }

        public virtual bool Remove(S selectable)
        {
            if (!selectables.Contains(selectable))
                return false;

            bool wasRemoved = selectables.Remove(selectable);

            if (wasRemoved)
                selectable.SetContainer<S, V>(null);

            return wasRemoved;
        }

        #endregion


        #region IPostBack

        public bool IsPostBack
        {
            get { return isPostBack; }
            set { isPostBack = value; }
        }

        #endregion


        #region Constructors

        public FormSelector(string name)
            : base(name)
        {
            selectables = new List<S>();

            IsPostBack = false;
        }

        #endregion
    }
}
