# Domain Services

This folder is reserved for **Domain Services**.

## When to add code here?
In Domain-Driven Design, most business logic should live inside **Entities** (like `Order`) or **Value Objects** (like `Price`). 
However, you should add a Domain Service here if a business rule:

1. **Involves Multiple Aggregates:** For example, a service that needs to check both `Product` stock and `Customer` credit before allowing an `Order` to be created.
2. **Depends on External State:** For example, a `TaxCalculationService` that needs to call an external API or look up complex regional tax tables.
3. **Doesn't "Fit" Naturally:** If a logic piece feels "forced" when put into the `Order` class, it likely belongs here.

## Current Status
In this tutorial, all business rules (like the Maximum Price Limit) are encapsulated within the **Order Aggregate Root** to demonstrate a "Rich Domain Model." No domain services are required for this specific implementation.