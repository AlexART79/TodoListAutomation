using System;
using System.Threading;

namespace CommonClasses
{
    public class Helpers
    {
        public static bool Wait(Func<bool> condition, TimeSpan treshold, int deleayMs = 1000)
        {
            var startTime = DateTime.Now;

            while (!condition())
            {                
                Thread.Sleep(deleayMs);

                var newTime = DateTime.Now;
                if (treshold <= newTime - startTime)
                {
                    // return false if timed out
                    return false;
                }
            }

            // if we got to this point, we are Ok
            return true;
        }

        public static bool Wait(Func<bool> condition)
        {
            // default wait timeout will be 30
            // todo: maybe will move this to config later...
            return Wait(condition, TimeSpan.FromSeconds(30));
        }
    }
}
