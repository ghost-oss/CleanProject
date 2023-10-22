using System;
using CleanProject.Domain.Common;

namespace CleanProject.Domain.Entities
{
    public class Department : AuditableEntity
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}

