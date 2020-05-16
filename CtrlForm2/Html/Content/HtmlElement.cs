using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using CtrlForm2.Html.Attributes.MultiValue;
using CtrlForm2.Html.Attributes.ReadOnly.String;
using CtrlForm2.Html.Attributes.Variable.Boolean;
using CtrlForm2.Html.Interfaces;

namespace CtrlForm2.Html.Content
{
    [SuppressMessage("Style", "IDE0028:Simplify collection initialization", Justification = "<Pending>")]

    public abstract class HtmlElement : HtmlContent
    {
        #region Fields

        protected readonly List<IHtmlAttribute> attributes;

        private readonly AttrId attrId;

        private readonly AttrClass attrClass;

        private readonly AttrHidden attrHidden;

        #endregion


        #region Properties

        public abstract string Tag
        {
            get;
        }

        protected virtual string Prefix
        {
            get { return ""; }
        }

        public IReadOnlyList<IHtmlAttribute> Attributes
        {
            get { return attributes; }
        }

        public int Depth
        {
            get { return container == null ? 1 : container.Depth + 1; }
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

            if (Prefix == null || Prefix.Trim().Length != Prefix.Length)
                throw new ApplicationException();

            attributes = new List<IHtmlAttribute>();

            attributes.Add(attrId = baseId != "" ? new AttrId(string.Format("{0}{1}", Prefix, baseId)) : new AttrId());

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
