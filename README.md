# Mastering Domain-Driven Design: A Tactical Implementation in .NET

[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)](https://dotnet.microsoft.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20%26%20DDD-green.svg)](https://medium.com/@aman.toumaj/mastering-domain-driven-design-a-tactical-ddd-implementation-5255d71d609f)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

> **Companion Repository for the Medium Article:** > [Mastering Domain-Driven Design: A Tactical DDD Implementation](https://medium.com/@aman.toumaj/mastering-domain-driven-design-a-tactical-ddd-implementation-5255d71d609f)

## 📖 Overview

This repository demonstrates a **production-grade implementation** of Tactical Domain-Driven Design (DDD) using .NET (targeting .NET 10 standards). It moves beyond simple "Hello World" examples to tackle real-world complexity, focusing on **Rich Domain Models**, **Encapsulation**, and **Automated Architectural Governance**.

The goal is to show how to build systems that are **secure by design**, maintainable, and strictly aligned with business rules.

## 🏗️ Architecture

The solution follows **Clean Architecture** principles with a strict dependency flow.

## Key Architectural Decisions

Domain-Centric: The Domain layer has zero dependencies. It contains strictly pure C# logic.

Rich Domain Models: No "Anemic" models with public setters. State changes occur only through behavioral methods (e.g., MarkAsShipped()).

CQRS with MediatR: Separation of Reads (Queries) and Writes (Commands).

Internal Visibility: Command Handlers are internal to the Application assembly to prevent leakage and enforce encapsulation.

## 🚀 Key Features

1. Advanced Value Objects
Value Objects are implemented as sealed records to ensure immutability and structural equality. We use custom operators for arithmetic logic (e.g., Money/Currency).

    ```csharp
    public sealed record Price(decimal Value, Currency Currency)
    {
        public static Price operator +(Price p1, Price p2) => ...
    }

2. Encapsulated Aggregates
Entities use private or protected setters and expose collections as IEnumerable to prevent external modification.

    ```csharp
    public class Order : AggregateRoot
    {
        // Collections are read-only to the outside world
        private readonly List<OrderItem> _orderItems = [];
        public IEnumerable<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public void AddOrderItem(OrderItem item)
        {
            // Business Invariants are checked here, not in the controller
            ValidateMaxPriceLimit(item);
            _orderItems.Add(item);
        }
    }

3. Automated Architecture Guards 🛡️
Instead of relying on manual code reviews, this project uses NetArchTest to enforce architectural rules via unit tests. If a developer breaks a rule (e.g., puts a Repository in the Domain layer), the build fails.

Guards included:

✅ Domain Entities must not have public setters.

✅ Repositories interfaces must reside in Domain.

✅ Persistence layer must not contain Interfaces (only implementations).

✅ Domain layer must not depend on Application or Infrastructure.

## 🛠️ Tech Stack

.NET 10

Entity Framework Core (Fluent API Configuration)

MediatR (In-process messaging)

FluentValidation (Input validation)

NetArchTest.Rules (Architecture testing)

xUnit & FluentAssertions (Testing)

SQL Server (Persistence)

## 🏃 Getting Started

Prerequisites

✅ NET SDK
✅ SQL Server (LocalDB or Docker)

Installation
1.  Clone the repository:

    ```bash
    git clone [https://github.com/tomajexpress/Domain.Driven.Implementation.In.CSharp.NET.Core.git](https://github.com/tomajexpress/Domain.Driven.Implementation.In.CSharp.NET.Core.git)

2.  Navigate to the solution folder and build:
    ```bash
    dotnet build

## 📂 Project Structure

| Project | Responsibility |
| :--- | :--- |
| **EShoppingTutorial.Core.Domain** | The heart of the software. Entities, Value Objects, Domain Services. |
| **EShoppingTutorial.Core.Application** | Use cases, CQRS Commands/Queries, Validators. |
| **EShoppingTutorial.Persistence** | EF Core DbContext, Repository Implementations, Migrations. |
| **EShoppingTutorial.API** | Minimal API Endpoints, Dependency Injection setup. |
| **EShoppingTutorial.ArchTests** | **The Guardians.** Tests ensuring architectural integrity. |

## 🤝 Contributing
Contributions are welcome! Please ensure that any PR maintains the "Green" status of the Architecture Tests.

## ✍️ Author
Aman Toumaj Senior Software Developer & Architecture Enthusiast

[![Medium](https://img.shields.io/badge/Medium-12100E?style=for-the-badge&logo=medium&logoColor=white)](https://medium.com/@aman.toumaj)
