using SmartwayTestTask.Database.Context;
using SmartwayTestTask.Repositories.Department;
using SmartwayTestTask.Repositories.Department.Interface;
using SmartwayTestTask.Repositories.Employees;
using SmartwayTestTask.Repositories.Employees.Interface;
using SmartwayTestTask.Repositories.Passport;
using SmartwayTestTask.Repositories.Passport.Interface;
using SmartwayTestTask.Services.Department;
using SmartwayTestTask.Services.Department.Interface;
using SmartwayTestTask.Services.Employees;
using SmartwayTestTask.Services.Employees.Interface;
using SmartwayTestTask.Services.Passport;
using SmartwayTestTask.Services.Passport.Interface;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<DatabaseContext>();

builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IPassportRepository, PassportRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IPassportService, PassportService>();

WebApplication application = builder.Build();
await application.Services.GetRequiredService<DatabaseContext>().InitDatabase();

application.MapControllers();
application.Run("https://localhost:7155");
