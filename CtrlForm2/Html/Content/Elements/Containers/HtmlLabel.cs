﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Html.Attributes.Variable.String;

namespace CtrlForm2.Html.Content.Elements.Containers
{
    public class HtmlLabel : HtmlContainer
    {
        #region Fields

        private readonly AttrFor attrFor;

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

        #endregion


        #region Constructors

        public HtmlLabel(string baseId)
            : base(baseId)
        {
            attributes.Add(attrFor = new AttrFor());
        }

        #endregion
    }
}