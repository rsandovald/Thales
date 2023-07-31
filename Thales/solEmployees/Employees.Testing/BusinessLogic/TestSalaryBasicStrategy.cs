using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employees.Entities;
using Employees.Strategies;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Constraints;

namespace Employees.Testing.BusinessLogic
{

    internal  class TestSalaryBasicStrategy
    {
        const string parameterSalaryBasicStrategy = "parameterSalaryBasicStrategy";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSalaryCalculation()
        {
            var builder = new ConfigurationBuilder();
            ulong salary = 1000000;
            ulong factor = 12;
            Employee employee; 

            var parameters = new Dictionary<string, string>();
            parameters.Add(parameterSalaryBasicStrategy, factor.ToString ());
            builder.AddInMemoryCollection(parameters);
            employee = new Employee()
            {
                Salary = salary,
                AnualSalary = 0
            };

            try
            {
                ISalaryStrategy salaryBasicStrategy = new SalaryBasicStrategy(builder.Build());
                salaryBasicStrategy.calculateAnualSalary(employee); 
            }
            catch (Exception)
            {
            }

            Assert.IsTrue( employee.AnualSalary == salary * factor);
        }

        [Test]
        public void TestConfigurationSuccess()
        {
            bool parametersAreOk = true;

            var builder = new ConfigurationBuilder();
            var parameters = new Dictionary<string, string>();
            parameters.Add(parameterSalaryBasicStrategy, "12");
            builder.AddInMemoryCollection(parameters);

            try
            {
                ISalaryStrategy salaryBasicStrategy = new SalaryBasicStrategy(builder.Build());
            }
            catch (Exception)
            {
                parametersAreOk = false;

            }            

            Assert.IsTrue (parametersAreOk);
        }

        [Test]
        public void TestConfigurationFailure()
        {
            bool parametersAreOk = true;

            var builder = new ConfigurationBuilder();
            var parameters = new Dictionary<string, string>();
            builder.AddInMemoryCollection(parameters);

            try
            {
                ISalaryStrategy salaryBasicStrategy = new SalaryBasicStrategy(builder.Build());
            }
            catch (Exception)
            {
                parametersAreOk = false;

            }

            Assert.IsTrue(!parametersAreOk);
        }
    }    
}
