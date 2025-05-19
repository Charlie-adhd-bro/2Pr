public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual List<Position> Positions { get; set; } = new();
}

public class Position
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; } = null!;

    public virtual List<Employee> Employees { get; set; } = new();
}

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int PositionId { get; set; }
    public virtual Position Position { get; set; } = null!;
}
