using EmployeeManagement.Data.Repositories;

string connectionString =
    @"Server=localhost\SQLEXPRESS;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

var repository = new EmployeeRepository(connectionString);

while (true)
{
    Console.Clear();

    Console.WriteLine("=== Employee Management ===");
    Console.WriteLine("1. View Employees");
    Console.WriteLine("2. Search Employee");
    Console.WriteLine("3. Exit");
    Console.Write("Enter choice: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            ViewEmployees(repository);
            break;

        case "2":
            SearchEmployee(repository);
            break;

        case "3":
            return;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Press any key...");
    Console.ReadKey();
}

static void ViewEmployees(EmployeeRepository repository)
{
    var employees = repository.GetEmployees();

    foreach (System.Data.DataRow row in employees.Rows)
    {
        Console.WriteLine(
            $"{row["Id"]} | " +
            $"{row["Name"]} | " +
            $"{row["Department"]} | " +
            $"{row["Salary"]}");
    }
}

static void SearchEmployee(EmployeeRepository repository)
{
    Console.Write("Enter employee name: ");
    string? name = Console.ReadLine();

    var employees = repository.GetEmployees();

    var result = employees.Select(
        $"Name LIKE '%{name?.Replace("'", "''")}%'");

    foreach (System.Data.DataRow row in result)
    {
        Console.WriteLine(
            $"{row["Id"]} | " +
            $"{row["Name"]} | " +
            $"{row["Department"]}");
    }
}