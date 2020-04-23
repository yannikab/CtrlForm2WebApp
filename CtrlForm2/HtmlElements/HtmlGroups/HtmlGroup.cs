using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using UserControls.CtrlForm2.HtmlElements.HtmlItems;
using UserControls.CtrlForm2.Interfaces;

namespace UserControls.CtrlForm2.HtmlElements.HtmlGroups
{
    public abstract class HtmlGroup : HtmlElement, IContainer
    {
        #region Fields

        private readonly List<IContainable> contents = new List<IContainable>();

        #endregion


        #region IContainer

        public IReadOnlyList<IContainable> Contents
        {
            get { return contents; }
        }

        public void Add(IContainable c)
        {
            if (c != null && c as HtmlGroup == null && c as HtmlItem == null)
                throw new ArgumentException();

            if (contents.Contains(c))
                return;

            contents.Add(c);

            c.Container = this;
        }

        public bool Remove(IContainable c)
        {
            if (c != null && c as HtmlGroup == null && c as HtmlItem == null)
                throw new ArgumentException();

            if (!contents.Contains(c))
                return false;

            c.Container = null;

            return contents.Remove(c);
        }

        #endregion


        #region Constructors

        public HtmlGroup(string baseId)
            : base(baseId)
        {
        }

        #endregion
    }
}
