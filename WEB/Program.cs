using Application.Interfaces;
using Application.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using WEB.Middlware;

namespace WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connection = builder.Configuration.GetConnectionString("MsSql");

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

            builder.Services.AddScoped<NotarService>();
            builder.Services.AddScoped<INotarRepository, NotarRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<ExceptionHandlingMiddlware>();

            app.Run();
        }
    }
}
