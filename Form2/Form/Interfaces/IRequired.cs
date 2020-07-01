using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Form.Interfaces
{
    interface IRequired
    {
        bool? Required { set; }

        bool IsRequired { get; }

        string RequiredMark { get; set; }

        string RequiredMessage { get; set; }

        bool IsRequiredMet { get; }
    }
}
