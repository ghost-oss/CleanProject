using System;
using CleanProject.Persistance.Context;
using CleanProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CleanProject.Application.Abstractions.Persistance;

namespace CleanProject.Persistance.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private ApplicationDbContext dbContext { get; set; }

        public EmployeeRespository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await dbContext.Employee.SingleOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task CreateEmployee(Employee employee)
        {
            await dbContext.Employee.AddAsync(employee);
            dbContext.SaveChanges();
        }
    }
}

