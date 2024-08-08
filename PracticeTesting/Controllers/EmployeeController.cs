using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PracticeTesting.Data;
using PracticeTesting.DTO;
using PracticeTesting.Models;
using PracticeTesting.Repository;
using System.Globalization;

namespace PracticeTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployee employee;
        private readonly EmployeeDbContext employeeDbContext;

        public EmployeeController(IEmployee employee, EmployeeDbContext employeeDbContext)
        {

            this.employee = employee;
            this.employeeDbContext = employeeDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee data)
        {
            if (data == null)
            {
                return BadRequest("no data");
            }
            var result = await employee.PostAsync(data);

            return Ok(result);
        }

        [HttpGet("Get All Data")]

        public async Task<List<Employee>> GetAll()
        {
            return await employee.GetAll();

        }

        [HttpPut("{id:Guid}")]

        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] Employee data)
        {


            var result = await employee.UpdateAsync(id, data);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);

        }

        [HttpDelete("{id:Guid}")]

        public async Task<IActionResult> Delete(Guid id)
        {


            var res = await employee.DeleteAsync(id);

            return Ok(res);
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await employee.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpGet("another get all")]
        public async Task<List<Employee>> GetAll1()
        {
            return await employeeDbContext.employees.ToListAsync();
        }

        [HttpGet("Another Get By Id{id:Guid}")]

        public async Task<IActionResult> GetById1([FromRoute] Guid id)
        {
            var IsIdExist = await employeeDbContext.employees.FindAsync(id);
            if (IsIdExist == null)
            {
                ModelState.AddModelError("", "Id Is Not Exist");
            }

            return Ok(IsIdExist);
        }

        [HttpPost("anotherpostmethod")]
        public async Task<IActionResult> Post1([FromBody] Employee employee)
        {
            var AddNewEmployee = new Employee()
            {
                EmployeeName = employee.EmployeeName,
                EmployeeCity = employee.EmployeeCity,
                EmployeeDepartment = employee.EmployeeDepartment,
            };

            await employeeDbContext.employees.AddAsync(AddNewEmployee);
            await employeeDbContext.SaveChangesAsync();
            return Ok("successfully");
        }

        [HttpPost("Add List Of Employees")]

        public async Task<IActionResult> AddListOfEmployees(List<Employee> employees)
        {
            foreach (var employee in employees)
            {
                var addnewemployee = new Employee()
                {
                    EmployeeName = employee.EmployeeName,
                    EmployeeCity = employee.EmployeeCity,
                    EmployeeDepartment = employee.EmployeeDepartment,
                };
                await employeeDbContext.employees.AddAsync(addnewemployee);
            }
            await employeeDbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("Filter")]
        public async Task<IActionResult> Filter([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            if (string.IsNullOrWhiteSpace(filterOn) || string.IsNullOrWhiteSpace(filterQuery)) {
                ModelState.AddModelError("", "error");
            }

            var result = employeeDbContext.employees.AsQueryable();

            if (filterOn.Equals("employeename", StringComparison.OrdinalIgnoreCase)) {

                result = result.Where(a => a.EmployeeName.Contains(filterQuery));


            }

            return Ok(result);

        }

        [HttpGet("Linq")]

        public async Task<IActionResult> Linq()
        {
            var result = employeeDbContext.employees.AsQueryable();

            result = result.OrderBy(a => a.EmployeeName);
            return Ok(result);
        }

        [HttpGet("linq where")]
        public async Task<IActionResult> WhereCondition()
        {
            var result = employeeDbContext.employees.AsQueryable();

            result = result.Where(a => a.EmployeeName.Length > 5).OrderByDescending(a => a.EmployeeName);

            return Ok(result);
        }

        [HttpGet("Stored Procedure")]

        public async Task<List<Employee>> GetEmployeeByIdAsync(Guid value)
        {
            var parameter = new SqlParameter("@value", value);
            return await employeeDbContext.employees
                .FromSqlRaw("EXEC selectbyinput @value", parameter)
                .ToListAsync();
        }

        [HttpGet("GroupBy")]
        //want to add the custom model class to get the result 
        public async Task<List<Employee>> GetByGroupBy(int id)
        {
            var parameter = new SqlParameter("@value", id);
            return await employeeDbContext.employees.FromSqlRaw("exec UpdatingByInput @value", parameter).ToListAsync();
        }

        [HttpPut("updateDepartment/{id:Guid}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] Guid id, [FromBody] UpdateEmployeeDepartmentDto updateEmployeeDepartmentDto)
        {
            var isEmloyeeExist = await employeeDbContext.employees.FindAsync(id);
            if (isEmloyeeExist == null)
            {
                return BadRequest("Not Found");
            }
            isEmloyeeExist.EmployeeDepartment=updateEmployeeDepartmentDto.EmployeeDepartment;
            employeeDbContext.employees.Update(isEmloyeeExist);
            await employeeDbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
