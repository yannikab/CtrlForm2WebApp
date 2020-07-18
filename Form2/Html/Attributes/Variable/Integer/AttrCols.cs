﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Attributes.Variable.Integer
{
    public class AttrCols : IntegerAttribute
    {
        #region Properties

        public override string Name
        {
            get { return "cols"; }
        }

        #endregion


        #region Constructors 

        public AttrCols(long cols)
            : base(cols)
        {
        }

        public AttrCols()
            : base()
        {
        }

        #endregion
    }
}
