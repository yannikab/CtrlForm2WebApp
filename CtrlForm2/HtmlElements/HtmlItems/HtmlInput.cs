using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.HtmlElements.HtmlAttributes;

namespace UserControls.CtrlForm2.HtmlElements.HtmlItems
{
    public abstract class HtmlInput : HtmlElement
    {
        #region Fields

        private readonly AttrName name;

        private readonly AttrReadOnly readOnly;

        private readonly AttrValue value;

        private readonly AttrType type;

        #endregion


        #region Properties

        public override string Tag
        {
            get { return "input"; }
        }

        public AttrName Name
        {
            get { return name; }
        }

        public AttrReadOnly ReadOnly
        {
            get { return readOnly; }
        }

        public AttrValue Value
        {
            get { return value; }
        }

        public AttrType Type
        {
            get { return type; }
        }

        #endregion


        #region Constructors

        public HtmlInput(string baseId, string type)
            : base(baseId)
        {
            name = new AttrName();

            readOnly = new AttrReadOnly();

            value = new AttrValue();

            this.type = new AttrType(type);
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0}: {1}, Value: {2}", GetType().Name, Id.Value, Value.Value);
        }

        #endregion

        //private static class Prefix
        //{
        //    public static string Get(InputType inputType)
        //    {
        //        switch (inputType)
        //        {
        //            case InputType.Password:
        //                return "pas";
        //            case InputType.CheckBox:
        //                return "cbx";
        //            case InputType.Radio:
        //                return "rdo";
        //            default:
        //                throw new ArgumentException();
        //        }
        //    }
        //}
    }
}
