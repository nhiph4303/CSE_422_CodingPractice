using System;
using System.Linq.Expressions;
using System.Reflection;

// =====================================================================
// ORIGINAL CODE (redundant)
// =====================================================================
public class Logger
{
    public void LogUserAction(string username, string action)
    {
        Console.WriteLine($"User {username} performed action: {action}");
    }

    public void LogTransaction(int transactionId, double amount)
    {
        Console.WriteLine($"Transaction {transactionId} processed with amount: {amount}");
    }

    public void LogError(string errorMessage, DateTime timestamp)
    {
        Console.WriteLine($"Error at {timestamp}: {errorMessage}");
    }
}

// =====================================================================
// APPROACH 1 – REFLECTION
// Dùng MethodBase.GetCurrentMethod() để lấy tên method và tên tham số
// tự động tại runtime, thay vì hard-code từng message.
// =====================================================================
public class ReflectionLogger
{
    public void LogUserAction(string username, string action)
        => Log(MethodBase.GetCurrentMethod()!, username, action);

    public void LogTransaction(int transactionId, double amount)
        => Log(MethodBase.GetCurrentMethod()!, transactionId, amount);

    public void LogError(string errorMessage, DateTime timestamp)
        => Log(MethodBase.GetCurrentMethod()!, errorMessage, timestamp);

    private static void Log(MethodBase method, params object?[] args)
    {
        ParameterInfo[] parameters = method.GetParameters();

        var parts = new System.Text.StringBuilder();
        for (int i = 0; i < parameters.Length; i++)
        {
            if (i > 0) parts.Append(", ");
            parts.Append($"{parameters[i].Name} = {args[i]}");
        }

        Console.WriteLine($"[Reflection] {method.Name} => {parts}");
    }
}

// =====================================================================
// APPROACH 2 – EXPRESSION TREES
// Caller truyền vào một lambda expression () => method(args).
// Expression Tree phân tích cú pháp của lambda để lấy tên method,
// tên tham số, và evaluate giá trị thực tế — không cần hard-code.
// =====================================================================
public static class ExpressionLogger
{
    public static void Log(Expression<Action> expression)
    {
        var call = (MethodCallExpression)expression.Body;

        string methodName = call.Method.Name;
        ParameterInfo[] parameters = call.Method.GetParameters();

        var parts = new System.Text.StringBuilder();
        for (int i = 0; i < parameters.Length; i++)
        {
            object? value = Expression.Lambda(call.Arguments[i]).Compile().DynamicInvoke();
            if (i > 0) parts.Append(", ");
            parts.Append($"{parameters[i].Name} = {value}");
        }

        Console.WriteLine($"[ExpressionTree] {methodName} => {parts}");
    }
}

// =====================================================================
// RUNNER
// =====================================================================
public static class Exercise1Runner
{
    public static void Run()
    {
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine(" Exercise 1 – Reflection & Expression Trees Logging");
        Console.WriteLine("══════════════════════════════════════════════════════\n");

        var logger          = new Logger();
        var reflLogger      = new ReflectionLogger();

        Console.WriteLine("─── Original Logger (redundant) ───");
        logger.LogUserAction("Alice", "Login");
        logger.LogTransaction(1001, 250.75);
        logger.LogError("Null reference exception", new DateTime(2026, 3, 7, 13, 0, 0));

        Console.WriteLine();

        Console.WriteLine("─── Refactored with Reflection ───");
        reflLogger.LogUserAction("Alice", "Login");
        reflLogger.LogTransaction(1001, 250.75);
        reflLogger.LogError("Null reference exception", new DateTime(2026, 3, 7, 13, 0, 0));

        Console.WriteLine();

        Console.WriteLine("─── Refactored with Expression Trees ───");
        ExpressionLogger.Log(() => logger.LogUserAction("Alice", "Login"));
        ExpressionLogger.Log(() => logger.LogTransaction(1001, 250.75));
        ExpressionLogger.Log(() => logger.LogError("Null reference exception", new DateTime(2026, 3, 7, 13, 0, 0)));

        Console.WriteLine();
        Console.WriteLine("══════════════════════════════════════════════════════");
    }
}
