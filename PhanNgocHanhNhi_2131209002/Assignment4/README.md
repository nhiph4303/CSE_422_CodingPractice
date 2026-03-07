# Notification System

A simple C# Console App for Assignment 4 using Factory, Strategy, and Decorator patterns.

## Project Structure

```text
NotificationSystem/
├── Interfaces/
│   └── INotifier.cs
├── Models/
│   └── NotificationType.cs
├── Notifiers/
│   └── ConcreteNotifiers.cs
├── Factories/
│   └── NotifierFactory.cs
├── Decorators/
│   ├── NotifierDecorator.cs
│   └── ConcreteDecorators.cs
├── Strategies/
│   └── NotificationContext.cs
├── Program.cs
└── README.md
```

## Design Decisions

### 1. Factory Pattern

Used to create different notifier objects (Email, SMS, Push, Task) based on an enum. It helps keep the creation logic separate from the rest of the app.

### 2. Strategy Pattern

The `NotificationContext` class lets us switch between different notification methods at runtime. This way, the client code doesn't need to know which specific notifier it's using.

### 3. Decorator Pattern

Implemented for Logging and Retry logic. These decorators wrap the core notifiers to add functionality without changing the original classes. You can even stack them (e.g., Logging on top of Retry).

## How to Run

Just run this command in the project folder:

```bash
dotnet run
```
