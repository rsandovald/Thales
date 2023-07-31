using Employees.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace Employees.Strategies
{
    public class SalaryBasicStrategy : ISalaryStrategy
    {
        const string parameterSalaryBasicStrategy  = "parameterSalaryBasicStrategy";
        private readonly IConfiguration _configuration;
        public SalaryBasicStrategy(IConfiguration configuration) 
        {
            bool isConfigurationValid;
            _configuration = configuration;
            isConfigurationValid = validateConfigurationParameters();

            if (!isConfigurationValid)
                throw new ArgumentNullException(parameterSalaryBasicStrategy);
        }

        public int Factor { get; set; }  
        public void calculateAnualSalary(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException();

            employee.AnualSalary = employee.Salary * (ulong)Factor; 
        }

        public void calculateAnualSalary(List<Employee> employees)
        {
            if (employees == null)
                throw new ArgumentNullException();

            foreach (Employee employee in employees) 
            {
                calculateAnualSalary(employee);
            }
        }
        private bool validateConfigurationParameters()
        {
            if (_configuration == null)
                return false;

            if (string.IsNullOrEmpty(_configuration[parameterSalaryBasicStrategy]))
                return false;

            Factor = int.Parse(_configuration[parameterSalaryBasicStrategy]);
            return true; 
        }

    }
}
