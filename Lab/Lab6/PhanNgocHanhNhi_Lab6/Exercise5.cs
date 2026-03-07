using System;
using System.Collections.Generic;

public class ProductService
{
    private Dictionary<int, string> _cache = new Dictionary<int, string>();

    public string GetProduct(int productId)
    {
        if (_cache.ContainsKey(productId))
        {
            Console.WriteLine("Fetching from cache...");
            return _cache[productId];
        }

        string product = $"Product {productId}";
        _cache[productId] = product;
        Console.WriteLine("Fetching from database...");
        return product;
    }
}

public class UserService
{
    private Dictionary<int, string> _cache = new Dictionary<int, string>();

    public string GetUser(int userId)
    {
        if (_cache.ContainsKey(userId))
        {
            Console.WriteLine("Fetching from cache...");
            return _cache[userId];
        }

        string user = $"User {userId}";
        _cache[userId] = user;
        Console.WriteLine("Fetching from database...");
        return user;
    }
}

public interface IDataService
{
    string Get(int id);
}

public class ProductDataService : IDataService
{
    public string Get(int id)
    {
        Console.WriteLine("Fetching from database...");
        return $"Product {id}";
    }
}

public class UserDataService : IDataService
{
    public string Get(int id)
    {
        Console.WriteLine("Fetching from database...");
        return $"User {id}";
    }
}

public class CachedDataService : IDataService
{
    private readonly IDataService _inner;
    private readonly Dictionary<int, string> _cache = new Dictionary<int, string>();

    public CachedDataService(IDataService inner)
    {
        _inner = inner;
    }

    public string Get(int id)
    {
        if (_cache.ContainsKey(id))
        {
            Console.WriteLine("Fetching from cache...");
            return _cache[id];
        }

        string result = _inner.Get(id);
        _cache[id] = result;
        return result;
    }
}

public static class Exercise5Runner
{
    public static void Run()
    {
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine(" Exercise 5 – Decorator Pattern for Cache Handling");
        Console.WriteLine("══════════════════════════════════════════════════════\n");

        Console.WriteLine("─── Original (cache logic duplicated in each service) ───");
        var productSvc = new ProductService();
        Console.WriteLine(productSvc.GetProduct(1));
        Console.WriteLine(productSvc.GetProduct(1));

        var userSvc = new UserService();
        Console.WriteLine(userSvc.GetUser(10));
        Console.WriteLine(userSvc.GetUser(10));

        Console.WriteLine();
        Console.WriteLine("─── Refactored with Decorator Pattern ───");

        IDataService cachedProduct = new CachedDataService(new ProductDataService());
        Console.WriteLine(cachedProduct.Get(1));
        Console.WriteLine(cachedProduct.Get(1));

        IDataService cachedUser = new CachedDataService(new UserDataService());
        Console.WriteLine(cachedUser.Get(10));
        Console.WriteLine(cachedUser.Get(10));

        Console.WriteLine();
        Console.WriteLine("══════════════════════════════════════════════════════");
    }
}
