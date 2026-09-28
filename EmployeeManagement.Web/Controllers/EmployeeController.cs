using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Core.Services;
using System.Data;

namespace EmployeeManagement.Web.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public IActionResult Index()
    {
        var employees = _employeeService.GetEmployees();

        return View(employees);
    }

    public IActionResult DataTablePractice()
    {
        DataTable table = _employeeService.GetEmployees();

        foreach (DataRow row in table.Rows)
        {
            Console.WriteLine($"{row["Id"]}-{row["Name"]}-{row["Department"]}");
        }

        DataRow[] itEmployees = table.Select("Department = 'IT'");
        // 3. Sort
        DataRow[] sortedEmployees =
            table.Select("", "Salary DESC");

        // 4. Filter + Sort
        DataRow[] activeEmployees =
            table.Select(
                "IsActive = true",
                "Salary DESC");
        Console.WriteLine($"Total: {table.Rows.Count}");
        Console.WriteLine($"IT Employees: {itEmployees.Length}");
        Console.WriteLine($"Sorted: {sortedEmployees.Length}");
        Console.WriteLine($"Active: {activeEmployees.Length}");
        return Content("Check the console output.");
    }


    [HttpGet]
    public IActionResult Search(string name)
    {
        DataTable table = _employeeService.GetEmployees();
        DataRow[] rows;
        if (string.IsNullOrWhiteSpace(name))
        {
            rows = table.Select();
        }
        else
        {
            rows = table.Select($"Name LIKE '%{name.Replace("'", "''")}%'");
        }
        var result = rows.Select(row =>
        new
        {
            Id = row["Id"],
            Name = row["Name"],
            Email = row["Email"],
            Department = row["Department"],
            Salary = row["Salary"]
        });
        return Json(result);
    }


    [HttpPost]
    public IActionResult Create(string name, string email, string department, decimal salary)
    {
        int id = _employeeService.AddEmployee(name, email, department, salary, DateTime.Now);

        return Json(new
        {
            success = true,
            id = id,
            message = "Employee added successfully"
        });
    }

    public IActionResult ITEmployees()
    {
        var employees = _service.GetEmployees();

        var result = _service.GetITEmployees(employees);

        return Content(
            $"IT Employees: {result.Length}");
    }



}