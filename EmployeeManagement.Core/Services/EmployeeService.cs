using System.Data;
using EmployeeManagement.Core.Repositories;

namespace EmployeeManagement.Core.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public DataTable GetEmployees()
    {
        return _repository.GetEmployees();
    }

    public DataRow[] GetITEmployees(DataTable employees)
    {
        return employees.Select("Department = 'IT'");
    }

    public int AddEmployee(string name, string email, string department, decimal salary, DateTime joiningDate)
    {
        return _repository.AddEmployee(name, email, department, salary, joiningDate);
    }
}