using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait
{
    internal class TaskVsValueTask
    {
       public async ValueTask<int> GetDataAsync()
        {
            bool resultAvailable = false;
            if (resultAvailable)
            {
                return 42;// Return synchrounsly -> may be cache can store data
            }
            else
            {
                await Task.Delay(5000);
                return 45;
            }
        }
        
    }
}
