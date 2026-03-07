# Technical Report: Library Management System - Design Patterns Implementation

**Course:** CSE 422 - Coding Practice
**Topic:** Applying Design Patterns in Library Management
**Student:** [Your Full Name]
**ID:** [Your Student ID]

---

## 1. Singleton Pattern: Database Connection

### Intent

To ensure that a class has only one instance and provides a global point of access to it. In this system, we use it for the `DatabaseConnection` to prevent resource leaks and redundant connection initializations.

### Implementation Details

- **Private Constructor:** Prevents direct instantiation.
- **Thread-safe Instance:** Uses a `lock` object and double-check locking mechanism to ensure safety in multi-threaded environments.
- **Global Access:** Provided via the `GetInstance()` method.

### SOLID Principles

- **Single Responsibility Principle:** The class is solely responsible for managing its own lifecycle and the specific database connection logic.

---

## 2. Factory Method Pattern: Document Management

### Intent

To define an interface for creating an object, but let subclasses decide which class to instantiate. This decouples the client code from the specific classes of documents (Book, Magazine, Newspaper).

### Implementation Details

- **`IDocument` Interface:** Defines common behavior for all documents.
- **`DocumentFactory`:** Contains a static method that returns an `IDocument` based on a string input, abstracting the instantiation logic.

### SOLID Principles

- **Open/Closed Principle:** New document types (e.g., EBook) can be added by creating a new class and updating the factory without changing existing client code.
- **Dependency Inversion Principle:** The client depends on the `IDocument` abstraction rather than concrete implementations.

---

## 3. Observer Pattern: Notification System

### Intent

To define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically.

### Implementation Details

- **`ISubscriber` Interface:** The observer interface.
- **`LibraryNotifier`:** The subject class that maintains a list of subscribers and notifies them via the `NotifySubscribers` method.
- **`UserSubscriber`:** Concrete observer that reacts to notifications.

### SOLID Principles

- **Interface Segregation Principle:** Subscribers only need to implement the `Update` method relevant to them.
- **Loose Coupling:** The notifier doesn't need to know the specific types of subscribers, only that they implement `ISubscriber`.

---

## 4. Strategy Pattern: Loan Fee Calculation

### Intent

To define a family of algorithms, encapsulate each one, and make them interchangeable. Strategy lets the algorithm vary independently from clients that use it.

### Implementation Details

- **`IFeeStrategy` Interface:** Common interface for all calculation algorithms.
- **Concrete Strategies:** `BookFeeStrategy` ($2000), `MagazineFeeStrategy` ($1500), and `NewspaperFeeStrategy` ($1000).
- **`Loan` Context:** Holds a reference to a strategy and can switch it dynamically at runtime using `SetStrategy`.

### SOLID Principles

- **Single Responsibility Principle:** Each strategy class encapsulates one specific calculation logic.
- **Open/Closed Principle:** New fee structures can be introduced by adding new strategy classes without modifying the `Loan` class.

---

## 5. Conclusion

By applying these patterns, the Library Management System is highly modular, easier to maintain, and follows professional software engineering standards. The architecture allows for scalability and robust resource management.
