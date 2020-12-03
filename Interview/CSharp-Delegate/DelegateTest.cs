using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDelegate
{
    public delegate void TestDelegate();
    public class DelegateTest
    {
        TestDelegate func;
        
        public void Execute()
        {
            func = () => Console.WriteLine("No Op");
            func += () => Console.WriteLine("No Op 2");
            func();
        }

    }
}
