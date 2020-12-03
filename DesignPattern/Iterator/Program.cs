using System;
using System.Security.Cryptography.X509Certificates;

namespace Iterator
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] persons = new Person[]{ new Person("Jon"), new Person("Dave") };
            People people = new People(persons);
            foreach (Person p in people)
            {
                Console.WriteLine(p.name);
            }

        }
    }
}
