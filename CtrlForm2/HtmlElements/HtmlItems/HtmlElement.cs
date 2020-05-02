using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using UserControls.CtrlForm2.HtmlElements.HtmlAttributes;

namespace UserControls.CtrlForm2.HtmlElements.HtmlItems
{
    public abstract class HtmlElement : HtmlItem
    {
        #region Fields

        private readonly AttrId attrId;

        private readonly AttrClass attrClass;

        private readonly AttrHidden attrHidden;

        #endregion


        #region Properties

        public abstract string Tag
        {
            get;
        }

        protected abstract string IdPrefix
        {
            get;
        }

        public AttrId Id
        {
            get { return attrId; }
        }

        public AttrClass Class
        {
            get { return attrClass; }
        }

        public AttrHidden Hidden
        {
            get { return attrHidden; }
        }

        #endregion


        #region Constructors

        public HtmlElement(string baseId)
        {
            if (baseId == null || baseId.Trim().Length != baseId.Length)
                throw new ArgumentException();

            if (string.IsNullOrEmpty(IdPrefix) || IdPrefix.Trim().Length != IdPrefix.Length)
                throw new ApplicationException();

            attributes.Add(attrId = new AttrId(baseId != "" ? string.Format("{0}{1}", IdPrefix, baseId) : null));

            attributes.Add(attrClass = new AttrClass());

            attributes.Add(attrHidden = new AttrHidden());
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0}: {1}", GetType().Name, Id.Value);
        }

        #endregion
    }
}
