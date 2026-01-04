
using AutoMapper;
using WebApi.Cognito.UserMng.ExternalServices;
using WebApi.Cognito.UserMng.Services;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting of the application");
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddTransient<IUserAppService, UserAppService>();
        builder.Services.AddTransient<ICognitoAppService, CognitoAppService>();
        // builder.Services.AddAutoMapper();

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
