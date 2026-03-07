using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public class Student : INamedEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

public class Teacher : INamedEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

public class StudentRepository
{
    private readonly IDbConnection _connection;

    public StudentRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public List<Student> GetAllStudents()
    {
        var students = new List<Student>();
        var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM Students";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            students.Add(new Student { Id = (int)reader[0], Name = (string)reader[1] });
        }
        reader.Close();
        return students;
    }
}

public class TeacherRepository
{
    private readonly IDbConnection _connection;

    public TeacherRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public List<Teacher> GetAllTeachers()
    {
        var teachers = new List<Teacher>();
        // Simulated query: SELECT * FROM Teachers
        var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM Teachers";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            teachers.Add(new Teacher { Id = (int)reader[0], Name = (string)reader[1] });
        }
        reader.Close();
        return teachers;
    }
}

public interface IEntity
{
    int Id { get; set; }
}

public interface IRepository<T> where T : class, IEntity
{
    List<T> GetAll();
    T? GetById(int id);
    void Add(T entity);
    void Delete(int id);
}

public class InMemoryRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly List<T> _store;

    public InMemoryRepository(List<T> seedData)
    {
        _store = seedData;
    }

    public List<T> GetAll() => new List<T>(_store);

    public T? GetById(int id) => _store.FirstOrDefault(e => e.Id == id);

    public void Add(T entity)
    {
        _store.Add(entity);
        Console.WriteLine($"[Repository] Added {typeof(T).Name}: Id={entity.Id}");
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            _store.Remove(entity);
            Console.WriteLine($"[Repository] Deleted {typeof(T).Name}: Id={id}");
        }
    }
}


public class StudentService
{
    private readonly IRepository<Student> _repository;

    public StudentService(IRepository<Student> repository)
    {
        _repository = repository;
    }

    public void PrintAll()
    {
        Console.WriteLine("  Students:");
        foreach (var s in _repository.GetAll())
            Console.WriteLine($"    Id={s.Id}, Name={s.Name}");
    }

    public void Add(Student student) => _repository.Add(student);
    public void Delete(int id)       => _repository.Delete(id);
}

public class TeacherService
{
    private readonly IRepository<Teacher> _repository;

    public TeacherService(IRepository<Teacher> repository)
    {
        _repository = repository;
    }

    public void PrintAll()
    {
        Console.WriteLine("  Teachers:");
        foreach (var t in _repository.GetAll())
            Console.WriteLine($"    Id={t.Id}, Name={t.Name}");
    }

    public void Add(Teacher teacher) => _repository.Add(teacher);
    public void Delete(int id)       => _repository.Delete(id);
}

// =====================================================================
// SIMPLE DI CONTAINER (manual composition root)
// =====================================================================
public static class ServiceContainer
{
    public static StudentService BuildStudentService()
    {
        var seedData = new List<Student>
        {
            new Student { Id = 1, Name = "Alice" },
            new Student { Id = 2, Name = "Bob" },
        };
        IRepository<Student> repo = new InMemoryRepository<Student>(seedData);
        return new StudentService(repo);
    }

    public static TeacherService BuildTeacherService()
    {
        var seedData = new List<Teacher>
        {
            new Teacher { Id = 1, Name = "Dr. Smith" },
            new Teacher { Id = 2, Name = "Prof. Johnson" },
        };
        IRepository<Teacher> repo = new InMemoryRepository<Teacher>(seedData);
        return new TeacherService(repo);
    }
}

public static class Exercise2Runner
{
    public static void Run()
    {
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine(" Exercise 2 – Generic Repository + Dependency Injection");
        Console.WriteLine("══════════════════════════════════════════════════════\n");

        var studentService = ServiceContainer.BuildStudentService();
        var teacherService = ServiceContainer.BuildTeacherService();

        Console.WriteLine("── GetAll ──");
        studentService.PrintAll();
        teacherService.PrintAll();

        Console.WriteLine();
        Console.WriteLine("── Add ──");
        studentService.Add(new Student { Id = 3, Name = "Charlie" });
        teacherService.Add(new Teacher { Id = 3, Name = "Dr. Lee" });

        Console.WriteLine();
        Console.WriteLine("── After Add ──");
        studentService.PrintAll();
        teacherService.PrintAll();

        Console.WriteLine();
        Console.WriteLine("── Delete (Id = 1) ──");
        studentService.Delete(1);
        teacherService.Delete(1);

        Console.WriteLine();
        Console.WriteLine("── After Delete ──");
        studentService.PrintAll();
        teacherService.PrintAll();

        Console.WriteLine();
        Console.WriteLine("══════════════════════════════════════════════════════");
    }
}
