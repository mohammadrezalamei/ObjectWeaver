# ObjectWeaver

[![NuGet version](https://img.shields.io/nuget/v/ObjectWeaver.svg)](https://www.nuget.org/packages/ObjectWeaver/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
ObjectWeaver is a lightweight and efficient object-to-object mapping library for .NET. Focused on simplicity and performance, it provides a straightforward way to map data between different object types in your applications.

## Features

* **Lightweight & Performant:** Designed with minimal overhead.
* **Convention-Based Mapping:** Automatically maps properties with matching names and compatible types.
* **Simple API:** Uses a clear and fluent extension method (`MapTo<T>`) for ease of use.
* **Custom Mapping Logic:** Supports complex scenarios via the `ICustomMapper<TSource, TDestination>` interface.
* **Flexible:** Handle scenarios requiring custom data transformations, lookups, or calculations during mapping.

## Installation

You can install ObjectWeaver via the .NET CLI or the NuGet Package Manager Console:

**.NET CLI**
```bash
dotnet add package ObjectWeaver
```
Package Manager Console

```PowerShell
Install-Package ObjectWeaver
```
Basic Usage
ObjectWeaver shines when mapping between objects with similar structures (properties with the same name and compatible types).

Example: Suppose you have a data model and a Data Transfer Object (DTO):

```C#

// Your data model
namespace ObjectWeaver.Test.Models;

public class PersonModel
{
    public System.Guid Id { get; set; }
    public System.String FirstName { get; set; }
    public System.String LastName { get; set; }
    public int Age { get; set; }
    public bool IsActive { get; set; }
}

// Your DTO
namespace ObjectWeaver.Test.Dtos;

public class PersonDto
{
    public System.Guid Id { get; set; }
    public System.String FirstName { get; set; }
    public System.String LastName { get; set; }
    public int Age { get; set; }
    public bool IsActive { get; set; }
}
```
You can easily map an instance of PersonModel to PersonDto:

```C#

using ObjectWeaver; // Make sure to import the necessary namespace
using ObjectWeaver.Test.Models;
using ObjectWeaver.Test.Dtos;

// 1. Create an instance of your source object
PersonModel model = new()
{
    Id = Guid.NewGuid(),
    FirstName = "MohammadReza",
    LastName = "Lamei",
    Age = 25,
    IsActive = true
};

// 2. Map it to the destination type using the MapTo extension method
PersonDto dto = model.MapTo<PersonDto>();

// Now 'dto' contains the data copied from 'model'
// dto.FirstName will be "MohammadReza", dto.Age will be 25, etc.
```
ObjectWeaver automatically handles the mapping because the property names (Id, FirstName, LastName, Age, IsActive) and their types match between PersonModel and PersonDto.

Custom Mapping with ICustomMapper
Often, your source and destination objects won't have perfectly matching property names, or you might need to perform custom logic during the mapping process (like combining fields, fetching data from elsewhere, or applying transformations). For these scenarios, ObjectWeaver provides the ICustomMapper interface.

Scenario: Let's map PersonModel to an EmployeeDto where property names differ

```C#

// Source class (same as before)
namespace ObjectWeaver.Test.Models;

public class PersonModel
{
    public System.Guid Id { get; set; }
    public System.String FirstName { get; set; }
    public System.String LastName { get; set; }
    public int Age { get; set; }
    public bool IsActive { get; set; }
}

// Destination class with different property names
namespace ObjectWeaver.Test.Dtos;

public class EmployeeDto
{
    public System.Guid EmployeeId { get; set; }       // Different name: Id -> EmployeeId
    public System.String EmployeeFirstName { get; set; } // Different name: FirstName -> EmployeeFirstName
    public System.String EmployeeLastName { get; set; }  // Different name: LastName -> EmployeeLastName
    public int EmployeeAge { get; set; }             // Different name: Age -> EmployeeAge
    public bool EmployeeIsActive { get; set; }        // Different name: IsActive -> EmployeeIsActive
    public System.String FullName => $"{EmployeeFirstName} {EmployeeLastName}"; // Calculated property
}
```
1. Create a Custom Mapper:

Implement the ICustomMapper interface. This interface requires you to define a Map method that takes the source object and returns a new instance of the destination object, containing your specific mapping logic.

```C#

using ObjectWeaver.Lib.Interface; // Make sure this namespace is correct for ICustomMapper
using ObjectWeaver.Test.Models;
using ObjectWeaver.Test.Dtos;

namespace ObjectWeaver.Test.CustomMappers;

public class PersonToEmployeeCustomMapper : ICustomMapper<PersonModel, EmployeeDto>
{
    public EmployeeDto Map(PersonModel source)
    {
        // Here you define EXACTLY how to create EmployeeDto from PersonModel
        return new EmployeeDto
        {
            EmployeeId = source.Id, // Map PersonModel.Id to EmployeeDto.EmployeeId
            EmployeeFirstName = source.FirstName,
            EmployeeLastName = source.LastName,
            EmployeeAge = source.Age,
            EmployeeIsActive = source.IsActive
            // Note: FullName is calculated automatically by EmployeeDto itself.
            // If FullName required complex logic based on source, you'd do it here.
        };
    }

    // You could also inject dependencies (like services or providers)
    // into your custom mapper's constructor if needed for complex lookups.
    // public PersonToEmployeeCustomMapper(IDataProvider provider) { ... }
}
```
Explanation:
The PersonToEmployeeCustomMapper class explicitly defines how each property in EmployeeDto should be populated from the PersonModel source. This gives you full control over the mapping process. You can:

Map properties with different names (Id to EmployeeId).
Perform calculations or transformations (source.Age * 2, source.FirstName.ToUpper(), etc.).
Combine multiple source properties into one destination property.
Fetch additional data from other services or repositories within the Map method (if you inject dependencies into your custom mapper).
Handle null checks or default values gracefully.
2. Use the Custom Mapper:

Pass an instance of your custom mapper to the overloaded MapTo extension method:

```C#

using ObjectWeaver;
using ObjectWeaver.Test.Models;
using ObjectWeaver.Test.Dtos;
using ObjectWeaver.Test.CustomMappers; // Import namespace for your custom mapper

// Assume 'model' is an instance of PersonModel (created as in the basic example)
PersonModel model = new() { /* ... initialize properties ... */ };

// Create an instance of your custom mapper
var customMapper = new PersonToEmployeeCustomMapper();

// Pass the mapper instance to the MapTo method
EmployeeDto dto = model.MapTo<PersonModel, EmployeeDto>(customMapper);

// 'dto' is now populated according to the logic defined in PersonToEmployeeCustomMapper
// dto.EmployeeId will have the value from model.Id
// dto.EmployeeFirstName will have the value from model.FirstName
// dto.FullName will be calculated based on the mapped first and last names.
```
By providing a custom mapper instance, you tell ObjectWeaver to use your specific implementation for this particular PersonModel to EmployeeDto mapping, bypassing the default convention-based approach.

Contributing
Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are greatly appreciated.   

ObjectWeaver is currently a simple library, and we welcome help in adding more features and improving existing ones.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!
