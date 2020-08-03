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

        public void Add(HtmlContent c)
        {
            if (c == null)
                throw new ArgumentNullException();

            if (c.Container == null)
            {
                if (contents.Contains(c))
                    throw new ApplicationException();

                contents.Add(c);

                if (!contents.Contains(c))
                    throw new ApplicationException();

                c.Container = this;
            }
            else if (ReferenceEquals(c.Container, this))
            {
                if (!contents.Contains(c))
                    throw new ApplicationException();

                return;
            }
            else
            {
                if (contents.Contains(c))
                    throw new ApplicationException();

                if (!c.Container.Remove(c))
                    throw new ApplicationException();
                
                contents.Add(c);

                if (!contents.Contains(c))
                    throw new ApplicationException();

                c.Container = this;
            }
        }

        public void Insert(int index, HtmlContent c)
        {
            if (index < 0 || index > contents.Count)
                throw new ArgumentOutOfRangeException();

            if (c == null)
                throw new ArgumentNullException();

            if (c.Container == null)
            {
                if (contents.Contains(c))
                    throw new ApplicationException();

                contents.Insert(index, c);

                if (!contents.Contains(c))
                    throw new ApplicationException();

                c.Container = this;
            }
            else if (ReferenceEquals(c.Container, this))
            {
                if (!contents.Contains(c))
                    throw new ApplicationException();

                return;
            }
            else
            {
                if (contents.Contains(c))
                    throw new ApplicationException();

                if (!c.Container.Remove(c))
                    throw new ApplicationException();

                contents.Insert(index, c);

                if (!contents.Contains(c))
                    throw new ApplicationException();

                c.Container = this;
            }
        }

        public bool Remove(HtmlContent c)
        {
            if (c == null)
                throw new ArgumentNullException();

            if (!contents.Contains(c))
            {
                if (c.Container == this)
                    throw new ApplicationException();

                return false;
            }

            bool removed = contents.Remove(c);

            if (removed)
                c.Container = null;

            return removed;
        }

        #endregion


        #region Constructors

        public HtmlContainer(string name)
            : base(name)
        {
            contents = contents = new List<HtmlContent>();
        }

        #endregion
    }
}
