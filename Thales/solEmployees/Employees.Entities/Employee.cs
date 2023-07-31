using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Entities
{
    public class Employee 
    {
        [Required]
        [StringLength(maximumLength: 10)]
        public string Id { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }
        public ulong Salary { get; set; }

        [Range(18, 120)]
        public byte  Age { get; set; }

        public string ProfileImage { get; set; }

        public ulong AnualSalary { get; set; }

    }
}
