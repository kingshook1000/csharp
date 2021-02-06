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

    public class Employee
    {
        public int EmployeeType;
        public int Bonus;
        public int MedicalAllowance;
        public int RentalAllowance;
        public Employee()
        {
            
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

        public int GetMonthlyMedication()
        {
            return 2000;
        }
    }

    public class ContractEmployee : IEmplyee
    {
        public int GetBonas()
        {
            return 10;
        }

        public int GetMonthlyRental()
        {
            return 1000;
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

    public abstract class BaseEmployeeFactory
    {
        protected Employee _employee;

        public BaseEmployeeFactory(Employee employee)
        {
            _employee = employee;
        }
        public abstract IEmplyee Create();

        public Employee ApplySalary()
        {
            IEmplyee empoyeeType = this.Create();
            _employee.Bonus = empoyeeType.GetBonas();
            return _employee;
        }
    }

    public class PermanentEmployeeFactory : BaseEmployeeFactory
    {
        public PermanentEmployeeFactory(Employee employee): base(employee)
        {
            
        }
        public override IEmplyee Create()
        {
            PermanentEmployee pe = new PermanentEmployee();

            _employee.MedicalAllowance = pe.GetMonthlyMedication();
            return pe;
        }
    }

}

