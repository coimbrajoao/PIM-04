using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Course.Data;
using Course.Models;
using Microsoft.AspNetCore.Identity;
using Course.Services;
using System;
using System.Threading.Tasks; // Para suporte a tarefas assíncronas Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder; // Para suporte à classe WebApplication
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models; // Para suporte ao Swagger


internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("FolhaConnection");

        builder.Services.AddDbContext<FolhaContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<FolhaContext>().AddDefaultTokenProviders();
        // Add services to the container.

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddScoped<UserServices>();

        


        builder.Services.AddControllers();
        builder.Services.AddMvc();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}