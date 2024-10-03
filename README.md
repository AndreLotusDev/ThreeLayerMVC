# ThreeLayerMVC

**ThreeLayerMVC** is a simple project designed to showcase a three-layer architecture using ASP.NET MVC or ASP.NET API. The project demonstrates a clear separation of concerns by dividing the application into three distinct layers: Presentation, Business/Service, and Data Access.

## Project Structure

1. **Presentation Layer (MVC or API)**  
   This layer is responsible for handling the user interface or the API endpoints. It serves as the entry point of the application, interacting with the business logic and presenting data to the users, whether it be through a web interface (MVC) or via RESTful services (API).

2. **Business/Service Layer**  
   This layer manages the core business logic of the application, acting as the bridge between the presentation layer and the data access layer. It ensures that the business rules are enforced, and it handles any cross-module interactions.  
   _Note:_ Future improvements will include examples of aggregate roots and more sophisticated module organization.

3. **Data Access Layer (PostgreSQL)**  
   This layer is responsible for interacting with the database (PostgreSQL in this example). It handles CRUD operations and ensures data persistence. The data access layer abstracts the database interactions, allowing the business layer to remain independent of specific data storage details.

## Technologies Used

- **ASP.NET MVC / ASP.NET API** for the presentation layer
- **C#** for the business logic and data access
- **Entity Framework Core** for ORM and database interactions
- **PostgreSQL** for the database

## Future Enhancements

- Improved examples of domain-driven design principles (aggregate root patterns)
- Better cross-module organization and handling
- Additional unit tests and integration tests for a more robust example

## How to Run

1. Clone the repository.
2. Set up a PostgreSQL database and update the connection strings in the appsettings.json file.
3. Build and run the application using Visual Studio or your preferred IDE.

---

<br/><br/><br/><br/>

# AppDbContext (Database Features)

## Key Features

### 1. **Entity Configuration**
   - The `OnModelCreating` method uses the `ModelBuilder` to apply configurations for each entity in the context.
   - It ensures that entity names, column names, and keys follow the **snake_case** naming convention by default through a custom `ToSnakeCase` method.
   - Applies configurations such as:
     - **Setting default string lengths**: All string columns are set to `varchar(100)` unless specified otherwise.
     - **Decimal precision**: Decimal properties are set to `decimal(18, 2)` precision by default.
     - **UTC DateTimes**: All `DateTime` properties are stored in **UTC** using PostgreSQL’s `timestamptz` data type.
     - **Default timestamps**: Properties like `CreatedAt` and `LastModifiedAt` are automatically set to `CURRENT_TIMESTAMP`.

### 2. **Global Delete Filter (Soft Delete)**
   - Implements a global query filter to apply soft deletes automatically for all entities that inherit from the `Entity` class.
   - Instead of physically deleting rows, entities are flagged as deleted using a boolean `Deleted` property. This ensures that data is retained for future reference or auditing.

### 3. **Restrict Delete Behavior**
   - Sets `DeleteBehavior.Restrict` as the default for all relationships, preventing cascading deletes by default. This allows for better control over related entities and helps maintain data integrity.

### 4. **Optimized Tracking**
   - Disables automatic change tracking with `ChangeTracker.AutoDetectChangesEnabled = false` to improve performance during bulk operations.
   - Query tracking is disabled by default with `ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking` to ensure read queries do not track entities unless explicitly requested.

### 5. **Versioning**
   - Configures optimistic concurrency handling by adding a `RowVersion` property to entities, using PostgreSQL’s `xid` type to handle version control.
   - Ensures that entities are updated only if their `RowVersion` matches the current version in the database, preventing race conditions during concurrent updates.

### 6. **Soft Delete Handling**
   - Overrides `SaveChangesAsync` to implement soft delete behavior. When an entity is deleted, it is marked as deleted (`Deleted = true`) and remains in the database.
   - This method recursively applies the soft delete to any related entities through navigation properties.

### 7. **Default Schema and Collation**
   - All tables are set to use the `default` schema in PostgreSQL.
   - Sets the collation to `ci_ai_collation`, which stands for **case-insensitive** and **accent-insensitive** string comparison.

## Future Improvements

- Implement user-based auditing to track who created or modified each entity (`CreatedByUserId`, `LastModifiedByUserId`).
- Add more granular control over soft delete behavior, allowing for optional physical deletion if needed.
