using DataLayer.Abstract;
using DataLayer.EntityFramework;
using DataLayer.Repository;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Abstract;
using ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// servisleri ekliyoruz (workerdaki gibi)
builder.Services.AddScoped<IWatcherService, WatcherService>();
builder.Services.AddScoped<IWatcherRepository, EFWatcher>();
builder.Services.AddScoped(typeof(IGeneric<>), typeof(GenericRepository<>));

// database baðlantýsý için dbcontext'i dahil ediyorum
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ServiceContext>(options => options.UseSqlServer(connectionString));


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
