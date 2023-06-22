using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using testAPI.EntityModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(); // Add this line to enable CORS

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<PokemonContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(builder => builder.AllowAnyOrigin()); // Add this line to allow any origin

app.UseAuthorization();

app.MapControllers();

app.Run();
