﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Form.Interfaces
{
    interface IHidden
    {
        bool? Hidden { set; }

        bool IsHidden { get; }
    }
}
