using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.HtmlElements.HtmlGroups;
using UserControls.CtrlForm2.Interfaces;

namespace UserControls.CtrlForm2.HtmlElements.HtmlItems
{
    public class HtmlItem : IContainable
    {
        #region Fields

        private IContainer container;

        #endregion


        #region Properties

        public int Depth
        {
            get { return (container as HtmlGroup) == null ? 1 : (container as HtmlGroup).Depth + 1; }
        }

        #endregion


        #region IContainable

        public IContainer Container
        {
            get { return container; }
            set
            {
                if (value != null && value as HtmlGroup == null)
                    throw new ArgumentException();

                container = value;
            }
        }

        #endregion
    }
}
