using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Form2.Html.Content.Elements
{
    public abstract class HtmlContainer : HtmlElement
    {
        #region Fields

        private readonly List<HtmlContent> contents;

        #endregion


        #region Properties

        public IReadOnlyList<HtmlContent> Contents
        {
            get { return contents; }
        }

        #endregion


        #region Methods

        public void Add(HtmlContent content)
        {
            if (contents.Contains(content))
                return;

            contents.Add(content);

            content.Container = this;
        }

        public bool Remove(HtmlContent content)
        {
            if (!contents.Contains(content))
                return false;

            content.Container = null;

            return contents.Remove(content);
        }

        #endregion


        #region Constructors

        public HtmlContainer(string baseId)
            : base(baseId)
        {
            contents = contents = new List<HtmlContent>();
        }

        #endregion
    }
}
