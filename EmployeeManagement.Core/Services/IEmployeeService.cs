using System.Data;

namespace EmployeeManagement.Core.Services;

public interface IEmployeeService
{
    DataTable GetEmployees();
    DataRow[] GetITEmployees(DataTable employees);

    int AddEmployee(string name, string email, string department, decimal salary, DateTime joiningDate);
}