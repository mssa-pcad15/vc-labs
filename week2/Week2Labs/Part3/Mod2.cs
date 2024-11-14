using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3
{
    internal class Mod2
    {
        internal static void VariableScope() {
            bool flag = true;
            if (flag)
            {
                int value = 10;
                Console.WriteLine($"Inside the code block: {value}");
            }

            //Console.WriteLine($"Outside the code block: {value}"); not allowed to see value,because
            // it is scoped to if code block
        }
    }
}
