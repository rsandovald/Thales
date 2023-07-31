using Employees.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.DTOs
{
    public  class ResponseGetEmployees : EmployeeDTO
    {
        public List<Employee> Employees { get; set; }
    }
}
