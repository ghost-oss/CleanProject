using System;
using CleanProject.Domain.Common;

namespace CleanProject.Domain.Entities
{
    public class Employee : AuditableEntity
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}

