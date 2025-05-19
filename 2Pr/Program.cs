using Microsoft.EntityFrameworkCore;

using (var db = new CompanyContext())
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    var devDept = new Department { Name = "Разработка" };
    var designDept = new Department { Name = "Дизайн" };

    var csharpPosition = new Position { Title = "Разработчик C#", Department = devDept };
    var javaPosition = new Position { Title = "Разработчик Java", Department = devDept };
    var uiDesigner = new Position { Title = "Дизайнер интерфейсов", Department = designDept };

    var emp1 = new Employee { Name = "Алиса", Position = csharpPosition };
    var emp2 = new Employee { Name = "Боб", Position = javaPosition };
    var emp3 = new Employee { Name = "Сан", Position = uiDesigner };
    var emp4 = new Employee { Name = "Чарли", Position = csharpPosition };

    db.Departments.AddRange(devDept, designDept);
    db.Positions.AddRange(csharpPosition, javaPosition, uiDesigner);
    db.Employees.AddRange(emp1, emp2, emp3, emp4);

    db.SaveChanges();
}
using (var db = new CompanyContext())
{
    Console.WriteLine("\t=== Lazy Loading ===");
    var employees = db.Employees.ToList();
    foreach (var emp in employees)
    {
        // Здесь при обращении к emp.Position и дальше к Department будет срабатывать lazy loading
        Console.WriteLine($"{emp.Name} — {emp.Position.Title} — {emp.Position.Department.Name}");
    }
}

using (var db = new CompanyContext())
{
    Console.WriteLine("\n\t=== Eager Loading ===");
    var employees = db.Employees
                      .Include(e => e.Position)
                      .ThenInclude(p => p.Department)
                      .ToList();
    foreach (var emp in employees)
    {
        Console.WriteLine($"{emp.Name} — {emp.Position.Title} — {emp.Position.Department.Name}");
    }
}

using (var db = new CompanyContext())
{
    Console.WriteLine("\n\t=== Explicit Loading ===");
    var employees = db.Employees.ToList();

    foreach (var emp in employees)
    {
        db.Entry(emp).Reference(e => e.Position).Load();
        db.Entry(emp.Position).Reference(p => p.Department).Load();

        Console.WriteLine($"{emp.Name} — {emp.Position.Title} — {emp.Position.Department.Name}");
    }
}
