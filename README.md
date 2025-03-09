# Entity Framework Core Setup Guide

## Steps to Set Up the Project

## SQL Setup

### 1. Open SQL Server Management Studio
### 2. Create Your Database and Tables
### 3. Make sure the Server is allow remote access
Right click on SqlServer not the database
Then click properties -> connections and make sure the remote access is allowed [checkbox]

## Project Setup

### 1. Open as Administrator (Optional)
Run your IDE or terminal as Administrator if necessary.

### 2. Set Startup Project
Ensure the project containing your context and models is set as the Startup Project.
Right click your project and click `Set as Startup Project`

### 3. Install Required Packages
Install the following NuGet packages:
1. `Microsoft.EntityFrameworkCore`
2. `Microsoft.EntityFrameworkCore.SqlServer`
3. `Microsoft.EntityFrameworkCore.Tools`

<img src="https://github.com/alcuinon/ASPNETMVCExampleWithDB/blob/main/Files/select_manage_nuget.gif" width="500">
<img src="https://github.com/alcuinon/ASPNETMVCExampleWithDB/blob/main/Files/browse_nuget.gif" width="500">

### 4. Open Package Manager Console
Navigate to **Tools -> NuGet Package Manager -> Package Manager Console** in your IDE.

### 5. Set Default Project
In the Package Manager Console, set the default project to your project name.
location: right corner of Pakage Manager Console

### 6. Scaffold the Database Context

Make sure you're in your project, use dir to show the directory
```powershell
dir
```
if you see your Project Name Folder then do this, proceed to `Scaffold-DbContext` if not.
```powershell
cd YourProjectName
```

<img src="https://github.com/alcuinon/ASPNETMVCExampleWithDB/blob/main/Files/dir_and_cd.gif" width="500">

Run the following command in the Package Manager Console, replacing `YourDBName` with the name of your database and `YourServerName` is located at the SQL Connect a Server:

```powershell
Scaffold-DbContext "Data Source=YourServerName;Initial Catalog=YourDBName;Integrated Security=True;Trust Server Certificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -ContextDir Context -Context MyDBContext -f
```

### 7. Update `appsettings.json`
Add the following section to your `appsettings.json` file, replacing `YourDBName` with your database name and `YourServerName` with your server name:

```json
"ConnectionStrings": {
    "Default": "Data Source=YourServerName;Initial Catalog=YourDBName;Integrated Security=True;Trust Server Certificate=True;"
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

# Usage (Example)

1. Go to HomeController
2. Add this code

```csharp
private readonly MyDBContext _context;

public HomeController(MyDBContext context)
{
    _context = context;
}
```

### How to get a list of records from database
```csharp
public IActionResult SomeMethod()
{
    var test = _context.Users.ToList();
}
```
### How to get a single record from database with condition
```csharp
public IActionResult SomeMethod()
{
    //if not found then test is null and test is a nullable datatype
    var test = _context.Users.FirstOrDefault(user => user.Id == 1);
    //if not found then this code is error
    var test2 = _context.Users.First(user => user.Id == 1);
    //if not found then test is null and test3 is a nullable datatype
    var test3 = _context.Users.SingleOrDefault(user => user.Id == 1);
    //if not found then this code is error
    var test4 = _context.Users.Single(user => user.Id == 1);
}
```
### How to add new record to database (Option 1)
```csharp
public IActionResult SomeMethod()
{
    _context.Users.Add(new User
    {
        Firstname = "Test",
        Lastname = "Test",
        Email = "Test@gmail.com",
    });

    //execute your changes to database
    _context.SaveChanges();
}
```

### How to add new record to database (Option 2)
```csharp
public IActionResult SomeMethod()
{
    var newUser = new User
    {
        Firstname = "Test",
        Lastname = "Test",
        Email = "Test@gmail.com",
    };
    
    _context.Users.Add(newUser);

    //execute your changes to database
    _context.SaveChanges();
}
```

### How to add multiple records to database
```csharp
public IActionResult SomeMethod()
{
    var newUsers = new List<User> 
    { 
        new User
        {
            Firstname = "Test1",
            Lastname = "Test1",
            Email = "Test1@gmail.com"
        },
        new User
        {
            Firstname = "Test2",
            Lastname = "Test2",
            Email = "Test2@gmail.com"
        }
    };
    
    _context.Users.AddRange(newUsers);
    
    //execute your changes to database
    _context.SaveChanges();
}
```

### How to update a record from database
```csharp
public IActionResult SomeMethod()
{
    var user = _context.Users.FirstOrDefault(user=> user.Id == 1);
    //if exist then proceed
    if(user is not null)
    {
        user.Email = "Test3@gmail.com"; //new value
        _context.Users.Update(user);
        //execute your changes to database
        _context.SaveChanges();
    }
}
```
### How to delete a record from database
```csharp
public IActionResult SomeMethod()
{
    var user1 = _context.Users.FirstOrDefault(user => user.Id == 1003);
    if (user1 is not null)
    {
        _context.Remove(user1);
        //execute your changes to database
        _context.SaveChanges();
    }
}
```
