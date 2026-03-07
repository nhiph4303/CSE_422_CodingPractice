using System;
using System.Collections.Generic;

public class PaymentService
{
    public void ProcessCreditCardPayment(double amount)
    {
        Console.WriteLine($"Processing credit card payment: {amount}");
    }

    public void ProcessPayPalPayment(double amount)
    {
        Console.WriteLine($"Processing PayPal payment: {amount}");
    }

    public void ProcessCryptoPayment(double amount)
    {
        Console.WriteLine($"Processing cryptocurrency payment: {amount}");
    }
}

public interface IPaymentStrategy
{
    void Process(double amount);
}

public class CreditCardPayment : IPaymentStrategy
{
    public void Process(double amount)
        => Console.WriteLine($"Processing credit card payment: {amount}");
}

public class PayPalPayment : IPaymentStrategy
{
    public void Process(double amount)
        => Console.WriteLine($"Processing PayPal payment: {amount}");
}

public class CryptoPayment : IPaymentStrategy
{
    public void Process(double amount)
        => Console.WriteLine($"Processing cryptocurrency payment: {amount}");
}

public static class PaymentFactory
{
    private static readonly Dictionary<string, Func<IPaymentStrategy>> _registry
        = new(StringComparer.OrdinalIgnoreCase)
        {
            ["creditcard"] = () => new CreditCardPayment(),
            ["paypal"]     = () => new PayPalPayment(),
            ["crypto"]     = () => new CryptoPayment(),
        };

    public static IPaymentStrategy Create(string method)
    {
        if (_registry.TryGetValue(method, out var factory))
            return factory();

        throw new NotSupportedException($"Payment method '{method}' is not supported.");
    }

    public static void Register(string method, Func<IPaymentStrategy> factory)
        => _registry[method] = factory;
}

public class PaymentProcessor
{
    public void Process(string method, double amount)
    {
        IPaymentStrategy strategy = PaymentFactory.Create(method);
        strategy.Process(amount);
    }
}

public static class Exercise4Runner
{
    public static void Run()
    {
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine(" Exercise 4 – Strategy Pattern + Factory Pattern");
        Console.WriteLine("══════════════════════════════════════════════════════\n");

        Console.WriteLine("─── Original PaymentService (redundant) ───");
        var original = new PaymentService();
        original.ProcessCreditCardPayment(100.0);
        original.ProcessPayPalPayment(200.0);
        original.ProcessCryptoPayment(300.0);

        Console.WriteLine();
        Console.WriteLine("─── Refactored PaymentProcessor (Strategy + Factory) ───");
        var processor = new PaymentProcessor();
        processor.Process("creditcard", 100.0);
        processor.Process("paypal",     200.0);
        processor.Process("crypto",     300.0);

        Console.WriteLine();
        Console.WriteLine("─── Adding a new payment method without changing existing code ───");
        PaymentFactory.Register("bankwire", () => new BankWirePayment());
        processor.Process("bankwire", 500.0);

        Console.WriteLine();
        Console.WriteLine("─── Unsupported method (exception handling) ───");
        try
        {
            processor.Process("bitcoin", 999.0);
        }
        catch (NotSupportedException ex)
        {
            Console.WriteLine($"  Error: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("══════════════════════════════════════════════════════");
    }
}

public class BankWirePayment : IPaymentStrategy
{
    public void Process(double amount)
        => Console.WriteLine($"Processing bank wire payment: {amount}");
}
