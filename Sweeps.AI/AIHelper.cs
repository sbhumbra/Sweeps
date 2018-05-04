using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweeps.AI
{
    static class AIHelper
    {
        /// <summary>
        /// Smaller is faster. Speed reprents the millisecond delay between each cycle
        /// </summary>
        const int Speed = 1000;

        public static async Task Pause()
        {
            await Task.Delay(Speed);
        }
    }
}
