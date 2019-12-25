# EFCore Examples

Examples to use with NetCore 3.0 and EFCore 3.0.

Context:

- Some branch office has **warehouses** and each
warehouse has many **products**.

- There are two types of products; **items** and
**non-items**. The **non-items products** are
compound by another products (that can be **item**
or **non-item**). The **item** product is a singular
product not compounded by another product.
Examples:
    - Printer, Keyboard and Mouse are **item products**
    - A store oferring a **Pack** consisting on
a Printer and two Keyboards is a **non-item product**
    
- Each **item product** has a **stock** in warehouses

The **examples** are stored in ```StockHouse/Examples/Example*``` folder
with `*` the index number.

## Prerequisites

- PostgreSQL 9.6+ and database created. Default database name: ```stock_house```, username: ```postgres``` and password: ```secret```.

- Update database with ```dotnet ef database update --project StockHouse}``` command.

## Run Main application

```bash
dotnet run --project StockHouse
```

## Run Test

```bash
dotnet test
```

## Anexo

### Añadir Migración

```bash
dotnet ef migrations add SchemaCreation --project StockHouse
```

### Ejecutar migraciones

```bash
dotnet ef database update --project StockHouse
```