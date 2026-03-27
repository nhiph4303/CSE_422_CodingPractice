# Notification System - Design Patterns Implementation

## 1. Project Overview
This project is a .NET Console Application designed to demonstrate the integration of
multiple Design Patterns (Factory, Strategy, and Decorator) into a unified notification
system. The system supports sending messages via Email, SMS, and Push Notifications
while providing extensible features like Logging.

## 2. Applied Design Patterns

### Strategy Pattern
- **Purpose**: To define a family of notification algorithms (Email, SMS, Push) and make them interchangeable.
- **Implementation**: Each notification type implements the `INotifier` interface. This allows the `Program` to switch between different notification methods without changing the execution logic.

### Factory Pattern
- **Purpose**: To centralize and encapsulate the object creation logic.
- **Implementation**: The `NotifierFactory` class provides a static method `CreateNotifier(string type)`. This decouples the client code from the concrete classes, making the system easier to maintain when new notification types are added.

### Decorator Pattern
- **Purpose**: To add additional responsibilities (Logging) to an object dynamically without modifying its structure.
- **Implementation**: The `LoggingDecorator` wraps an instance of `INotifier`. It adds timestamped logs before and after the core `Send()` method execution. This follows the **Open/Closed Principle**.

## 3. Folder Structure & Separation of Concerns
The project is organized into specific folders to ensure high maintainability and readability:

- **`/Interfaces`**: Contains `INotifier.cs`. This is the "Contract" that all notification types and decorators must follow.
- **`/Models`**: Contains concrete implementations (`EmailNotifier`, `SmsNotifier`, `PushNotifier`). This separates the core business logic of each channel.
- **`/Factories`**: Contains `NotifierFactory.cs`. This handles the instantiation logic, separating "how an object is created" from "how it is used".
- **`/Decorators`**: Contains `NotifierDecorator.cs` (Base) and `LoggingDecorator.cs`. This allows for "Plug-and-Play" features that can be wrapped around any notifier.

## 4. Design Decisions
- **Scalability**: To add a new method like "WhatsApp", we only need to create a new class in `/Models` and add one case to the `Factory`. No existing code in `Program.cs` or `Decorators` needs to change.
- **Extensibility**: If we need a "Retry" mechanism, we can simply add a `RetryDecorator` without touching the `LoggingDecorator` or the base Notifiers.
- **Clean Code**: By using Dependency Injection (passing objects into constructors), the code remains loosely coupled and easy to Unit Test.
