﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Html.Attributes.ReadOnly.String;
using CtrlForm2.Html.Attributes.Variable.Boolean;

namespace CtrlForm2.Html.Elements.Containers
{
    public class HtmlSubmit : HtmlContainer
    {
        #region fields

        private readonly AttrType attrType;

        private readonly AttrDisabled attrDisabled;

        #endregion


        #region Properties

        public override string Tag
        {
            get { return "button"; }
        }

        protected override string IdPrefix
        {
            get { return "btn"; }
        }

        public AttrType Type
        {
            get { return attrType; }
        }

        public AttrDisabled Disabled
        {
            get { return attrDisabled; }
        }

        #endregion


        #region Constructors

        public HtmlSubmit(string baseId)
            : base(baseId)
        {
            attributes.Add(attrType = new AttrType("submit"));

            attributes.Add(attrDisabled = new AttrDisabled());
        }

        #endregion
    }
}
