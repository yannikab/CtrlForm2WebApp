using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using UserControls.CtrlForm2.HtmlElements.HtmlAttributes;
using UserControls.CtrlForm2.Interfaces;

namespace UserControls.CtrlForm2.HtmlElements.HtmlItems
{
    public abstract class HtmlElement : HtmlItem //, IClassAtribute
    {
        #region Fields

        //protected readonly List<string> classNames;

        private readonly AttrId id;

        private readonly AttrClass @class;

        private readonly AttrHidden hidden;

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
            get { return id; }
        }

        public AttrClass Class
        {
            get { return @class; }
        }

        public AttrHidden Hidden
        {
            get { return hidden; }
        }

        #endregion


        //#region IClassAttribute

        //public IReadOnlyList<string> ClassNames
        //{
        //    get { return classNames; }
        //}

        //public void AddClassName(string className)
        //{
        //    if (new Regex("-?[_a-zA-Z]+[_a-zA-Z0-9-]*").IsMatch(className) == false)
        //        throw new ArgumentException();

        //    classNames.Add(className.Trim());
        //}

        //#endregion


        #region Constructors

        public HtmlElement(string baseId)
        {
            if (baseId == null || baseId.Trim().Length != baseId.Length)
                throw new ArgumentException();

            if (string.IsNullOrEmpty(IdPrefix) || IdPrefix.Trim().Length != IdPrefix.Length)
                throw new ApplicationException();

            id = new AttrId(baseId != "" ? string.Format("{0}{1}", IdPrefix, baseId) : null);

            //classNames = new List<string>();

            @class = new AttrClass();

            hidden = new AttrHidden();
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
