﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.String
{
    public class AttrPlaceHolder : StringAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "placeholder"; }
        }

        #endregion


        #region Constructors

        public AttrPlaceHolder(string value)
            : base(value)
        {
        }

        public AttrPlaceHolder()
            : this(null)
        {
        }

        #endregion
    }
}