﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserControls.CtrlForm2.Interfaces
{
    public interface IContainable
    {
        IContainer Container { get; set;  }
    }
}
