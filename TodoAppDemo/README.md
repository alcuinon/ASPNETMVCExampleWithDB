

1. Open as Administrator(optional)
2. Make sure the project you put the context and models are as StartUp Project
3. Install
    1. Microsoft.EntityFrameworkCore
    2. Microsoft.EntityFrameworkCore.SqlServer
    3. Microsoft.EntityFrameworkCore.Tools
4. Open Package Manager Console at the Tools->Nuget Package Manager -> Console
5. On the Package Manager Console make sure the default project should be the name of your project.
6. Type This below [change the YourDBName on your db name]

Scaffold-DbContext "Server=localhost;Database=YourDBName;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -ContextDir Context -Context MyDBContext -f

7. Go to appsettings.json and add this

"ConnectionStrings": {
        "Default": "Server=localhost;Database=SchoolDB;Trusted_Connection=True;"
    }

make sure to change the database name [YourDBName] is just an example

8. Go to Program.cs and type this before the "var app = builder.Build();" line.
    builder.Services.AddDbContext<MyDBContext>(options =>
                options
                    .UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:Default").Value,
                        sql => sql.EnableRetryOnFailure())
                    .EnableSensitiveDataLogging(), ServiceLifetime.Transient
                );

    make sure to add the reference folder thru using

