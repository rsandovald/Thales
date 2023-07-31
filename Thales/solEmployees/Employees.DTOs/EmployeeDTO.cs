using Employees.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.DTOs
{
    public abstract class EmployeeDTO
    {
        public TransactionResult TransactionResult { get; set; }
    }
}
