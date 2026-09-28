using System.Data;
using Microsoft.Data.SqlClient;
using EmployeeManagement.Core.Repositories;

namespace EmployeeManagement.Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly string _connectionString;

    public EmployeeRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DataTable GetEmployees()
    {
        DataTable table = new DataTable();

        using SqlConnection connection = new SqlConnection(_connectionString);

        string query = @"
            SELECT 
                Id,
                Name,
                Email,
                Department,
                Salary,
                JoiningDate,
                IsActive
            FROM Employees";

        using SqlCommand command = new SqlCommand(query, connection);

        using SqlDataAdapter adapter = new SqlDataAdapter(command);

        adapter.Fill(table);

        return table;
    }

    public int AddEmployee(
        string name,
        string email,
        string department,
        decimal salary,
        DateTime joiningDate)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);

        string query = @"
            INSERT INTO Employees
            (Name, Email, Department, Salary, JoiningDate, IsActive)
            VALUES
            (@Name, @Email, @Department, @Salary, @JoiningDate, 1);

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Name", name);
        command.Parameters.AddWithValue("@Email", email);
        command.Parameters.AddWithValue("@Department", department);
        command.Parameters.AddWithValue("@Salary", salary);
        command.Parameters.AddWithValue("@JoiningDate", joiningDate);

        connection.Open();

        return (int)command.ExecuteScalar()!;
    }
}