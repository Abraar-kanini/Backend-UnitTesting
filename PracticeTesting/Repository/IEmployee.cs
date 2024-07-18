using Microsoft.AspNetCore.Mvc;
using PracticeTesting.Models;

namespace PracticeTesting.Repository
{
    public interface IEmployee
    {

        Task<List<Employee>> GetAll();
        Task<Employee> PostAsync(Employee employee);

        Task<Employee> UpdateAsync(Guid id, Employee employee);
        Task<Employee> DeleteAsync(Guid id);

        Task<Employee> GetById(Guid id);
    }
}
