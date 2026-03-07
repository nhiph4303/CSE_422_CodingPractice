using System;
public interface IActionResult
{
    void Execute();
}

public class OkResult : IActionResult
{
    private readonly string _message;
    public OkResult(string message) { _message = message; }
    public void Execute() => Console.WriteLine($"  200 OK: {_message}");
}

public class BadRequestResult : IActionResult
{
    private readonly string _error;
    public BadRequestResult(string error) { _error = error; }
    public void Execute() => Console.WriteLine($"  400 Bad Request: {_error}");
}

public abstract class ControllerBase
{
    protected IActionResult Ok(string message)    => new OkResult(message);
    protected IActionResult BadRequest(string error) => new BadRequestResult(error);
}

public interface INamedEntity : IEntity
{
    string Name { get; set; }
}

public class StudentController : ControllerBase
{
    public IActionResult CreateStudent(Student student)
    {
        if (student == null || string.IsNullOrEmpty(student.Name))
            return BadRequest("Invalid data");
        return Ok($"Student {student.Name} created successfully.");
    }
}

public class TeacherController : ControllerBase
{
    public IActionResult CreateTeacher(Teacher teacher)
    {
        if (teacher == null || string.IsNullOrEmpty(teacher.Name))
            return BadRequest("Invalid data");
        return Ok($"Teacher {teacher.Name} created successfully.");
    }
}

public abstract class GenericController<T> : ControllerBase where T : class, INamedEntity
{
    public IActionResult Create(T entity)
    {
        if (entity == null || string.IsNullOrEmpty(entity.Name))
            return BadRequest("Invalid data");
        return Ok($"{typeof(T).Name} {entity.Name} created successfully.");
    }

    public IActionResult GetById(T entity)
    {
        if (entity == null)
            return BadRequest("Entity not found");
        return Ok($"Found {typeof(T).Name}: Id={entity.Id}, Name={entity.Name}");
    }
}

public class StudentApiController : GenericController<Student> { }
public class TeacherApiController : GenericController<Teacher> { }

public static class Exercise3Runner
{
    public static void Run()
    {
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine(" Exercise 3 – Generic Base Controller + Constraints");
        Console.WriteLine("══════════════════════════════════════════════════════\n");

        Console.WriteLine("─── Original Controllers (redundant) ───");
        var origStudent = new StudentController();
        var origTeacher = new TeacherController();

        origStudent.CreateStudent(new Student { Id = 1, Name = "Alice" }).Execute();
        origStudent.CreateStudent(new Student { Id = 2, Name = "" }).Execute();
        origTeacher.CreateTeacher(new Teacher { Id = 1, Name = "Dr. Smith" }).Execute();
        origTeacher.CreateTeacher(null!).Execute();

        Console.WriteLine();
        Console.WriteLine("─── Generic Controllers (no duplicated logic) ───");
        var studentApi = new StudentApiController();
        var teacherApi = new TeacherApiController();

        studentApi.Create(new Student { Id = 1, Name = "Alice" }).Execute();
        studentApi.Create(new Student { Id = 2, Name = "" }).Execute();
        teacherApi.Create(new Teacher { Id = 1, Name = "Dr. Smith" }).Execute();
        teacherApi.Create(null!).Execute();

        Console.WriteLine();
        Console.WriteLine("─── GetById (bonus – shared by all controllers via base) ───");
        studentApi.GetById(new Student { Id = 1, Name = "Alice" }).Execute();
        teacherApi.GetById(new Teacher { Id = 1, Name = "Dr. Smith" }).Execute();

        Console.WriteLine();
        Console.WriteLine("══════════════════════════════════════════════════════");
    }
}
