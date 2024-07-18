using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using PracticeTesting.Data;
using PracticeTesting.Models;

namespace PracticeTesting.Repository
{
    public class EmployeeRepo : IEmployee
    {
        private readonly EmployeeDbContext employeeDbContext;

        public EmployeeRepo(EmployeeDbContext employeeDbContext)
        {
            this.employeeDbContext = employeeDbContext;
        }
        public async Task<Employee> PostAsync(Employee employee)
        {
            var data = new Employee
            {
                EmployeeName = employee.EmployeeName,
                EmployeeCity = employee.EmployeeCity,
                EmployeeDepartment = employee.EmployeeDepartment,
            };

            await employeeDbContext.employees.AddAsync(data);
            await employeeDbContext.SaveChangesAsync();

            return data;

            
        }

        public async Task<Employee> DeleteAsync(Guid id)
        {
            var result = await employeeDbContext.employees.FindAsync(id);
            employeeDbContext.Remove(result);
            await employeeDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<List<Employee>> GetAll()
        {
            var result = await employeeDbContext.employees.ToListAsync();
            return result;
        }

        public async Task<Employee> UpdateAsync(Guid id, Employee employee)
        {
            var empid = await employeeDbContext.employees.FindAsync(id);
            


            empid.EmployeeName = employee.EmployeeName;
            empid.EmployeeCity = employee.EmployeeCity;
            empid.EmployeeDepartment = employee.EmployeeDepartment;

            employeeDbContext.Update(empid);
            await employeeDbContext.SaveChangesAsync();

            return empid;
        }

        public async Task<Employee> GetById(Guid id)
        {
            var result= await employeeDbContext.employees.FindAsync(id);

            
            return result;
        }
    }
}
