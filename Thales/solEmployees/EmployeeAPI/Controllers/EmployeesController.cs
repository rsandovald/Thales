using Employees.Repositories;
using Employees.Strategies;
using Employees.Entities; 
using Microsoft.AspNetCore.Mvc;
using Employees.DTOs;
using System.Text;

namespace EmployeeAPI.Controllers
{

    [ApiController]
    [Route("employees")]
    public class EmployeesController : ControllerBase
    {
        IRepository _repositoryEmployees;
        ISalaryStrategy _salaryBasicStrategy;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController (IRepository repositoryRestApiDummy,
                                    ISalaryStrategy salaryBasicStrategy,
                                    ILogger<EmployeesController> logger)  
        {
            _repositoryEmployees = repositoryRestApiDummy;
            _salaryBasicStrategy = salaryBasicStrategy;
            _logger = logger;
        }   

        [HttpGet]
        public async Task<ActionResult<ResponseGetEmployees>> GetAll()
        {
            ResponseGetEmployees result; 

            try
            {
                var employees = await _repositoryEmployees.GetAll();

                if (employees != null && employees.Count > 0)
                {
                    _salaryBasicStrategy.calculateAnualSalary(employees);
                    result = new ResponseGetEmployees()
                    {
                        TransactionResult = new TransactionResult()
                        {
                            Code = "200",
                            Description = "Successfull"
                        },
                        Employees = employees
                    };
                    return  Ok(result);
                }
               
                result = new ResponseGetEmployees()
                {
                    TransactionResult = new TransactionResult()
                    {
                        Code = "400",
                        Description = "Not Found"
                    }
                };
                
                return Ok (result);
            }
            catch (Exception ex)
            {
                logError(ex);
                result = new ResponseGetEmployees()
                {
                    TransactionResult = new TransactionResult()
                    {
                        Code = "500",
                        Description = "Internal Error"
                    }
                };
                return StatusCode(500, result); 
            }   
        }


        [HttpGet("{Id:int}")] // employees/1
        public async Task<ActionResult<ResponseGetEmployeeById>> GetById (string Id)
        {
            ResponseGetEmployeeById result;

            try
            {
                var employee = await _repositoryEmployees.GetById (Id);

                if (employee != null)
                {
                    _salaryBasicStrategy.calculateAnualSalary(employee);

                    result = new ResponseGetEmployeeById()
                    {
                        TransactionResult = new TransactionResult()
                        {
                            Code = "200",
                            Description = "Successfull"
                        },
                        Employee = employee
                    };

                    return Ok (result);
                }
                result = new ResponseGetEmployeeById()
                {
                    TransactionResult = new TransactionResult()
                    {
                        Code = "400",
                        Description = "Not Found"
                    },
                };

                return Ok(result); 

            }
            catch (Exception ex)
            {
                logError (ex);
                result = new ResponseGetEmployeeById()
                {
                    TransactionResult = new TransactionResult()
                    {
                        Code = "500",
                        Description = "Internal Error"
                    }
                };
                return StatusCode(500, result);
            }
        }

        private void logError (Exception ex)
        {
            StringBuilder message = new StringBuilder ();
            
            message.Append (ex.Message);
            message.Append (ex.StackTrace);

            _logger.LogError(message.ToString());
        }
    }
}
