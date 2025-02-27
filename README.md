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
Run the following command in the Package Manager Console, replacing `YourDBName` with the name of your database:
```powershell
Scaffold-DbContext "Server=localhost;Database=YourDBName;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -ContextDir Context -Context MyDBContext -f
```
or if not working do this
```powershell
Scaffold-DbContext "Data Source=YourServerName;Initial Catalog=YourDBName;Integrated Security=True;Trust Server Certificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -ContextDir Context -Context MyDBContext -f
```

### 6.1 How to Get the Connection String in your Local DB (if step #6 not working)

if you don't know how to get this `Data Source=YourServerName;Initial Catalog=YourDBName;Integrated Security=True;Trust Server Certificate=True`
1. `Data Source=YourServerName` is the Server Name
1. `Initial Catalog=YourDBName` is the Database Name

to get the Connection String do this

1. On your Visual Studio Go to View -> Click Server Explorer
2. It will show a side panel, click the Connect to Database Icon
3. Select Microsoft SQL Server (sometimes this was already selected)
4. Click the Down arrow on Server Name Field, wait until it shows the server name then click the server name
5. Authentication should be Windows Authentication
6. Mark check the Trust Server Certificate
7. Select Database name by clicking the down arrow icon, wait again and choose the database
8. optional, you can try to Test Connection
9. Click OK, and it will show on your Server Explorer Panel
10. Right Click on your database, click properties
11. It will prompt an another side panel called `Properties` and look for Connection String field
12. Copy that Connection String and paste that on the Scaffold command and try again the `STEP #6`


### 7. Update `appsettings.json`
Add the following section to your `appsettings.json` file, replacing `YourDBName` with your database name:

```json
"ConnectionStrings": {
    "Default": "Server=localhost;Database=YourDBName;Trusted_Connection=True;"
}
```
if you do Step #6.1 then do this intead
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
