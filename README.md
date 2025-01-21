# Entity Framework Core Setup Guide

## Steps to Set Up the Project

### 1. Open as Administrator (Optional)
Run your IDE or terminal as Administrator if necessary.

### 2. Set Startup Project
Ensure the project containing your context and models is set as the Startup Project.

### 3. Install Required Packages
Install the following NuGet packages:
1. `Microsoft.EntityFrameworkCore`
2. `Microsoft.EntityFrameworkCore.SqlServer`
3. `Microsoft.EntityFrameworkCore.Tools`

### 4. Open Package Manager Console
Navigate to **Tools -> NuGet Package Manager -> Package Manager Console** in your IDE.

### 5. Set Default Project
In the Package Manager Console, set the default project to your project name.

### 6. Scaffold the Database Context
Run the following command in the Package Manager Console, replacing `YourDBName` with the name of your database:

```powershell
Scaffold-DbContext "Server=localhost;Database=YourDBName;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -ContextDir Context -Context MyDBContext -f
```

### 7. Update `appsettings.json`
Add the following section to your `appsettings.json` file, replacing `YourDBName` with your database name:

```json
"ConnectionStrings": {
    "Default": "Server=localhost;Database=YourDBName;Trusted_Connection=True;"
}
```

### 8. Configure the Database Context in `Program.cs`

Before the line `var app = builder.Build();`, add the following code:

```csharp
builder.Services.AddDbContext<MyDBContext>(options =>
    options
        .UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:Default").Value,
            sql => sql.EnableRetryOnFailure())
        .EnableSensitiveDataLogging(), 
    ServiceLifetime.Transient
);
```

Note
Ensure you include the necessary using directives for the referenced namespaces.
