using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using CtrlForm2.Html.Elements.Items;

namespace CtrlForm2.Html.Elements.Containers
{
    public abstract class HtmlContainer : HtmlElement
    {
        #region Fields

        private readonly List<HtmlItem> items;

        #endregion


        #region Properties

        public IReadOnlyList<HtmlItem> Items
        {
            get { return items; }
        }

        #endregion


        #region Methods

        public void Add(HtmlItem item)
        {
            if (items.Contains(item))
                return;

            items.Add(item);

            item.Container = this;
        }

        public bool Remove(HtmlItem item)
        {
            if (!items.Contains(item))
                return false;

            item.Container = null;

            return items.Remove(item);
        }

        #endregion


        #region Constructors

        public HtmlContainer(string baseId)
            : base(baseId)
        {
            items = items = new List<HtmlItem>();
        }

        #endregion
    }
}
