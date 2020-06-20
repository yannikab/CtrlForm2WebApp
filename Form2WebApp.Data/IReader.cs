using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Schematrix.Data
{
    public interface IReader<T> : IDisposable, IEnumerable<T>
    {
        bool Read();
        T Current { get; }
        void Close();
        List<T> ToList();
        DataTable ToDataTable();
    }
}
