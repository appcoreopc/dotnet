using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogger
{
    public interface ILogger<T>
    {
        bool Log(T app);

        IEnumerable<T> GetLogs(int maxSize);

        //IEnumerable<T> GetLogs(DateTime start, DateTime end);

    }
}
