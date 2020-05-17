using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Content.Items;

namespace Form2.Form.Interfaces
{
    public interface IValidate
    {
        string ValidationMessage { get; }
        
        bool IsValid { get; }
    }

    public interface IValidate<F> : IValidate
    {
        Func<F, string> Validator { get; set; }

        Action<F> ActionInvalid { get; set; }
    }
}
