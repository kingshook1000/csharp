using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayTest
{
    public class IfThen
    {
        public IfThen() { }

        public void TestIfThen()
        {
            var a = false;
            var b = true;
            if (a == true)
                if (b == true)
                    Console.WriteLine("here");
            else
                Console.WriteLine("I am here");

        }
    }
}
