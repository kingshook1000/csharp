using System;

namespace TestConstructor
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://stackoverflow.com/questions/42997409/instantiating-an-object-but-using-curly-brackets-instead-of-the-default-constru
            Console.WriteLine("Testing  Instantiation");
            //Options options = new Options { Dob = "2020-12-12", Name = "Jon" };
            Type1 obj1 = new Type1(new Options { Dob = "2020-12-12", Name = "Jon" });
            Type1 obj2 = new Type1(new Options(false) { Dob = "2020-12-12", Name = "Jon" });
        }
    }

    public class Type1
    {
        Options options;
        public Type1(Options options)
        {
            this.options = options;
        }
    }

    public class Options
    {
        bool isTest;
        public string Name { get; set; }

        public string Dob { get; set; }
        public Options()
        { }
        public Options(bool isTest)
        {
            this.isTest = isTest;
        }

    }
}
