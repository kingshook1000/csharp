using System;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeData = new Employee {
                EmployeeType = 1,
            };

            BaseEmployeeFactory employeeFactory = new EmployeeFactory2().CreateFactory(employeeData);
            employeeFactory.ApplySalary();


        }

        
    }

    public class Employee
    {
        public int EmployeeType;
        public int Salary;
        public int Bonus;
        public int ExtraBenefit;
        public int Overtime;
        public Employee()
        {
            
        }
    }
    
    public interface IEmplyee
    {
        public int GetBonas();
        public int BasePay();


    }
    public class PermanentEmployee : IEmplyee
    {
        int IEmplyee.GetBonas()
        {
            return 100;
        }

        int IEmplyee.BasePay()
        {
            return 1500;
        }

        public int GetBenefit()
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

        int IEmplyee.BasePay()
        {
            return 1000;
        }

        public int GetOvertime()
        {
            return 500;
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
            IEmplyee anyEmployee = this.Create();
            _employee.Bonus = anyEmployee.GetBonas();
            _employee.Salary = anyEmployee.BasePay();
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

            _employee.ExtraBenefit = pe.GetBenefit();
            return pe;
        }
    }

    public class ContractEmployeeFactory : BaseEmployeeFactory
    {
        public ContractEmployeeFactory(Employee emplyee) : base(emplyee)
        { }

        public override IEmplyee Create()
        {
            ContractEmployee ce = new ContractEmployee();
            _employee.Overtime = ce.GetOvertime();
            return ce;
        }
    }

    public class EmployeeFactory2
    {
        public BaseEmployeeFactory CreateFactory(Employee employee)
        {
            if (employee.EmployeeType == 1)
                return new PermanentEmployeeFactory(employee);
            else if (employee.EmployeeType == 2)
                return new ContractEmployeeFactory(employee);

            return null;
        }
    }

}

