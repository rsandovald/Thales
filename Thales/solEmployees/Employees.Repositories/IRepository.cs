using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employees.Entities;

namespace Employees.Repositories
{
    public  interface IRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetById(string id);
    }
}
