using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.Interfaces
{
    interface IRequired
    {
        bool IsRequired { get; set; }

        string RequiredMark { get; set; }
    }
}
