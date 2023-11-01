
using BackEndProject.Interfaces;
using BackEndProject.Services;
using BackEndProject.Services.Common;

namespace EmployeeProject;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(typeof(ApplicationProfile));

        IConfiguration configuration = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json")
                                            .Build();

        builder.Services.AddTransient<IEmployeeRepository>(x=>new EmployeeRepository(configuration.GetConnectionString("EmployeeConnection")));
        builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
        builder.Services.AddTransient(x => new StudentService(configuration.GetConnectionString("DapperConnection")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}