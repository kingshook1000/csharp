using System;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        
    }
    
    public interface IEmplyee
    {
        public int GetBonas();

        
    }
    public class PermanentEmployee : IEmplyee
    {
        int IEmplyee.GetBonas()
        {
            return 100;
        }
    }

    public class ContractEmployee : IEmplyee
    {
        public int GetBonas()
        {
            return 10;
        }
    }

    public class EmployeeFactory
    {
        public IEmplyee CreateEmployee(int employeeType)
        {
            if (employeeType == 1)
            {
                return new PermanentEmployee();
            }
            else if (employeeType == 2)
            {
                return new PermanentEmployee();
            }

            return null;

        }
    }

}

