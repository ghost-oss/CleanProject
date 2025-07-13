using CleanProject.Domain.Entities;

namespace CleanProject.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeById(int id);

        public Task CreateEmployee(Employee employee);
    }
}

