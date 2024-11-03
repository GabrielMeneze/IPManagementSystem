using IPManagementSystem.Application;
using IPManagementSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers and swagger configuration
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configs to Database and Cache
builder.Services.AddApplicationServices();     
builder.Services.AddInfrastructureServices(builder.Configuration);

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
