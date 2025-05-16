using Group_Expense_Tracker.Server;
using Group_Expense_Tracker.Server.Data;
using Group_Expense_Tracker.Server.Data.Interfaces;
using Group_Expense_Tracker.Server.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<TrackerDbContext>(options =>
    options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("MyDb"))
);

builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TrackerDbContext>();
    context.Database.EnsureCreated();
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
