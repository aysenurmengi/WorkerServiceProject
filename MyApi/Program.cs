using DataLayer.Abstract;
using DataLayer.EntityFramework;
using DataLayer.Repository;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Abstract;
using ServiceLayer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Authentication i�in gereken k�s�m
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{ // kimlik do�rulama t�r�n�n Jwt'yi temel ald���n�n� belirtiyor, bearer-> token ile do�rulama olaca��n� s�yler
    options.TokenValidationParameters = new TokenValidationParameters // jwt'nin do�rulanmas�nda kullan�lacak parametreler
    {
        ValidateIssuer = true, // olu�turan yetkiliyi
        ValidateAudience = true, // hedef kitlesini
        ValidateLifetime = true, // token s�resini
        ValidateIssuerSigningKey = true, // imzalamak i�in gerekli olan anahtar�
        ValidIssuer = builder.Configuration["AppSettings:Issuer"], // olu�turucusunun ge�erli olup olmad���n�
        ValidAudience = builder.Configuration["AppSettings:Audience"], // hedef kitlenin ge�erlili�i
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]))
    };
});

// servisleri ekliyoruz (workerdaki gibi)
builder.Services.AddScoped<IWatcherService, WatcherService>();
builder.Services.AddScoped<IWatcherRepository, EFWatcher>();
builder.Services.AddScoped(typeof(IGeneric<>), typeof(GenericRepository<>));


// database ba�lant�s� i�in dbcontext'i dahil ediyorum
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

app.UseAuthentication(); // ikisinin s�ras� �nemliymi�
app.UseAuthorization();

app.MapControllers();

app.Run();
