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


//Authentication için gereken kýsým
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{ // kimlik doðrulama türünün Jwt'yi temel aldýðýnýný belirtiyor, bearer-> token ile doðrulama olacaðýný söyler
    options.TokenValidationParameters = new TokenValidationParameters // jwt'nin doðrulanmasýnda kullanýlacak parametreler
    {
        ValidateIssuer = true, // oluþturan yetkiliyi
        ValidateAudience = true, // hedef kitlesini
        ValidateLifetime = true, // token süresini
        ValidateIssuerSigningKey = true, // imzalamak için gerekli olan anahtarý
        ValidIssuer = builder.Configuration["AppSettings:Issuer"], // oluþturucusunun geçerli olup olmadýðýný
        ValidAudience = builder.Configuration["AppSettings:Audience"], // hedef kitlenin geçerliliði
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]))
    };
});

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

app.UseAuthentication(); // ikisinin sýrasý önemliymiþ
app.UseAuthorization();

app.MapControllers();

app.Run();
