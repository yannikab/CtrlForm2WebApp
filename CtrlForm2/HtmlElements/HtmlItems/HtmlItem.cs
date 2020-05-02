using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.HtmlElements.HtmlGroups;
using UserControls.CtrlForm2.Interfaces;

namespace UserControls.CtrlForm2.HtmlElements.HtmlItems
{
    public abstract class HtmlItem
    {
        #region Fields

        private HtmlContainer container;

        protected readonly List<IHtmlAttribute> attributes;

        #endregion


        #region Properties

        public HtmlContainer Container
        {
            get { return container; }
            set { container = value; }
        }

        public IReadOnlyList<IHtmlAttribute> Attributes
        {
            get { return attributes; }
        }

        public int Depth
        {
            get { return container == null ? 1 : container.Depth + 1; }
        }

        #endregion


        #region Constructors

        public HtmlItem()
        {
            attributes = new List<IHtmlAttribute>();
        }

        #endregion
    }
}
