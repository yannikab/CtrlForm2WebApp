using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using CtrlForm2.Html.Attributes.MultiValue;
using CtrlForm2.Html.Attributes.ReadOnly.String;
using CtrlForm2.Html.Attributes.Variable.Boolean;

namespace CtrlForm2.Html.Elements.Items
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

        protected virtual string IdPrefix
        {
            get { return ""; }
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

            if (IdPrefix == null || IdPrefix.Trim().Length != IdPrefix.Length)
                throw new ApplicationException();

            attributes.Add(attrId = baseId != "" ? new AttrId(string.Format("{0}{1}", IdPrefix, baseId)) : new AttrId());

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
