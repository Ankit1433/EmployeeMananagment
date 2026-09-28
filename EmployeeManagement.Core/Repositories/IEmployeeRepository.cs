using System.Data;

namespace EmployeeManagement.Core.Repositories;

public interface IEmployeeRepository
{
    DataTable GetEmployees();
    int AddEmployee(string name, string email, string department,decimal salary, DateTime joiningDate);
}