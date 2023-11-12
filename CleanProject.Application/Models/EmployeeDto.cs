using System;
using System.Text.Json.Serialization;

namespace CleanProject.Application.Models
{
    public class EmployeeDTO
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DateOfBirth { get; set; }

        public int DepartmentId { get; set; }
    }
}