using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Employees.Repositories.Models.RestApiDummy
{
    public  class ResponseGetEmployeesDto
    {
        public string status { get; set; }
        public List<EmployeeDto> data { get; set; }
        public string message { get; set; }
    }

    public class ResponseGetEmployeeByIdDto
    {
        public string status { get; set; }
        public EmployeeDto data { get; set; }
        public string message { get; set; }
    }

    public class EmployeeDto
    {
        public string id { get; set; }
        public string employee_name { get; set; }
        public ulong employee_salary { get; set; }
        public byte employee_age { get; set; }
        public string profile_image { get; set; }
    }



}
