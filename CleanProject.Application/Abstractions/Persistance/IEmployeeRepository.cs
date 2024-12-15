using System;
using CleanProject.Domain.Entities;

namespace CleanProject.Application.Abstractions.Persistance
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeById(int id);

        public Task CreateEmployee(Employee employee);
    }
}

