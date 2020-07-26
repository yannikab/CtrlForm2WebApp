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

        string RequiredMessage { get; set; }

        string RequiredMark { get; set; }

        bool? RequiredInLabel { set; }
        bool IsRequiredInLabel { get; }

        bool? RequiredInPlaceholder { set; }
        bool IsRequiredInPlaceholder { get; }

        string OptionalMark { get; set; }

        bool? OptionalInLabel { set; }
        bool IsOptionalInLabel { get; }

        bool? OptionalInPlaceholder { set; }
        bool IsOptionalInPlaceholder { get; }
    }
}
