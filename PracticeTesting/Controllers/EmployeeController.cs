using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeTesting.Data;
using PracticeTesting.Models;
using PracticeTesting.Repository;

namespace PracticeTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
       
        private readonly IEmployee employee;

        public EmployeeController(IEmployee employee)
        {
            
            this.employee = employee;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee data)
        {
            if (data == null)
            {
                return BadRequest("no data");
            }
           var result= await employee.PostAsync(data);

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
            

           var result= await employee.UpdateAsync(id,data);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);   

        }

        [HttpDelete("{id:Guid}")]

        public async Task<IActionResult> Delete (Guid id)
        {
            

          var res=  await employee.DeleteAsync(id);

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

    }
}
