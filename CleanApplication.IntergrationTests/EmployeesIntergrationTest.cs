using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CleanApplication.IntergrationTests;

[TestFixture]
public class EmployeesIntergrationTest
{
    private CustomWebApplicationFactory factory;

    public EmployeesIntergrationTest()
    {
        factory = new CustomWebApplicationFactory();
        factory.Services.EnsureCreatedAndSeed(dbcontext =>
        {
            dbcontext.Employee.Add(new CleanProject.Domain.Entities.Employee
            {
                EmployeeId = 2,
                FirstName = "Testyyyy",
                LastName = "BLAH",
                DateOfBirth = DateTime.Now
            });

            dbcontext.SaveChanges();
        });
    }

    [SetUp]
    public void Setup()
    {
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        await factory.DisposeAsync();
    }

    [Test]
    public async Task Test1()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("api/employees");

        var output = await response.Content.ReadAsStringAsync();

        var concrete = JsonConvert.DeserializeObject<List<EmployeeDTO>>(output);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(concrete.First().FirstName, Is.EqualTo("sahil"));
        });
    }

    [Test]
    public async Task Test2()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("api/employees/2");

        var output = await response.Content.ReadAsStringAsync();

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        });
    }
}

public class EmployeeDTO
{
    [System.Text.Json.Serialization.JsonIgnore]
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