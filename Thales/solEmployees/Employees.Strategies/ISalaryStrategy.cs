using Employees.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Strategies
{
    public  interface ISalaryStrategy
    {
        void calculateAnualSalary (Employee employee);
        void calculateAnualSalary (List <Employee> employees);

    }
}
