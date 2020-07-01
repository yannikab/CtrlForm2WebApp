using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Form.Interfaces
{
    interface IReadOnly
    {
        bool? ReadOnly { set; }

        bool IsReadOnly { get; }
    }
}
