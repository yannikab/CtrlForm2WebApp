using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Form.Interfaces
{
    interface IReadOnly
    {
        bool? IsReadOnly { get; set; }
    }
}
