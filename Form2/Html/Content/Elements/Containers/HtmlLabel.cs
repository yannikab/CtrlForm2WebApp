using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Attributes.Variable.String;

namespace Form2.Html.Content.Elements.Containers
{
    public class HtmlLabel : HtmlContainer
    {
        #region Fields

        private readonly AttrFor attrFor;

        private bool tabs;

        private bool lineBreak;

        #endregion


        #region Properties

        public override string Tag
        {
            get { return "label"; }
        }

        protected override string Prefix
        {
            get { return "lbl"; }
        }

        public AttrFor For
        {
            get { return attrFor; }
        }

        public bool Tabs
        {
            get { return tabs; }
            set { tabs = value; }
        }

        public bool LineBreak
        {
            get { return lineBreak; }
            set { lineBreak = value; }
        }

        #endregion


        #region Constructors

        public HtmlLabel(string name)
            : base(name)
        {
            attributes.Add(attrFor = new AttrFor());

            tabs = true;
            lineBreak = true;
        }

        #endregion
    }
}
