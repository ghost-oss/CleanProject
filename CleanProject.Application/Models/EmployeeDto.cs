using System;
using System.Text.Json.Serialization;

namespace CleanProject.Application.Models
{
    public class EmployeeDTO
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonPropertyName("departmentId")]
        public int DepartmentId { get; set; }
    }
}