using Employees.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Employees.Repositories.Models.RestApiDummy;

namespace Employees.Repositories
{
    public class RepositoryRestApiDummy : IRepository
    {
        const string parameterBaseUrl = "DummyRestApiBaseUrl";
        const string mediaTypeJson = "application/json";
        const string getEmployeesPath = "employees";
        const string getEmployeeByIdPath = "employee/{id}";
        const string statusSuccess = "success";

        private readonly IConfiguration _configuration;

        public RepositoryRestApiDummy(IConfiguration configuration)
        {
            bool isConfigurationValid; 
            _configuration = configuration;
            isConfigurationValid = validateConfigurationParameters();

            if (!isConfigurationValid)
                throw new ArgumentNullException(parameterBaseUrl); 
        }

        public async Task<List<Entities.Employee>> GetAll()
        {
            var responseGetEmployees = await GetAllEmployees();

            if (responseGetEmployees != null && responseGetEmployees.status != statusSuccess)
                //TODO: change exception type
                throw new Exception();

            return mapEmployees(responseGetEmployees.data); 
        }

        public async Task<Entities.Employee> GetById(string id)
        {
            var ResponseGetEmployeeById = await GetEmployeeById(id);

            if (ResponseGetEmployeeById != null && ResponseGetEmployeeById.status != statusSuccess)
                //TODO: change exception type
                throw new Exception();

            var result  = mapEmployee(ResponseGetEmployeeById.data);
            return result; 
        }

        public string BaseUrl { get; set; }

        private bool validateConfigurationParameters ()
        {
            
            if (_configuration == null) 
                return false; 

            if (string.IsNullOrEmpty (_configuration[parameterBaseUrl]))  
                return false;

            BaseUrl = _configuration[parameterBaseUrl];
            return true; 
        }

        public async Task<ResponseGetEmployeesDto> GetAllEmployees()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);

            var content = new StringContent(string.Empty, Encoding.UTF8, mediaTypeJson);
            var response = await client.GetAsync(getEmployeesPath);
            var serviceResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseGetEmployeesDto>(serviceResult);

            return result;
        }

        public async Task<ResponseGetEmployeeByIdDto> GetEmployeeById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);

            var content = new StringContent(string.Empty, Encoding.UTF8, mediaTypeJson);
            var response = await client.GetAsync(getEmployeeByIdPath.Replace ("{id}", id));   
            var serviceResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseGetEmployeeByIdDto>(serviceResult);

            return result;
        }


        public List<Entities.Employee> mapEmployees (List<Employees.Repositories.Models.RestApiDummy.EmployeeDto> employees)
        {
            if (employees == null)
                return new List<Employee>(); 

            return employees.Select (e => mapEmployee (e)).ToList ();  
        }

        public Entities.Employee mapEmployee(Employees.Repositories.Models.RestApiDummy.EmployeeDto employee)
        {
            if (employee == null)
                return null; 

            return new Entities.Employee()
                {
                    Id = employee.id,
                    Name = employee.employee_name,
                    Salary = employee.employee_salary,
                    Age = employee.employee_age,
                    ProfileImage = employee.profile_image                              
                };             
        }
    }

}
