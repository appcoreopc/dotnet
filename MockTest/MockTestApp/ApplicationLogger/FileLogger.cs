using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogger
{
    public class FileLogger : ILogger<string>
    {
        public bool Log(string content)
        {
            bool state = false; 
            try
            {
                File.AppendAllText("c:\\test.log,", content);
                state = true;
            }
           catch (Exception ex)
            {
               
            }
            return state;
        }
        
        public IEnumerable<string> GetLogs(int max)
        {
            return new List<string>().AsEnumerable();
        }
    }
}
