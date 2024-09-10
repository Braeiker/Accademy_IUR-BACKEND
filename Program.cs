using IUR_Backend.Data;
using IUR_Backend.Models;
using IUR_Backend.Models.Service;
using IUR_Backend.Settings;
using IUR_Backend.Settings.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura i servizi
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura Identity
builder.Services.AddIdentity<ApplicationUser, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configura JWT
var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<Jwt>(jwtSection);

var jwtSettings = jwtSection.Get<Jwt>();
builder.Services.AddSingleton(jwtSettings); // Aggiungi Jwt come singleton

// Configura le impostazioni di Identity
var identitySection = builder.Configuration.GetSection("Identity");
builder.Services.Configure<Identity>(identitySection);

var identitySettings = identitySection.Get<Identity>();
builder.Services.AddSingleton(identitySettings); // Aggiungi Identity come singleton

// Registrazione dei gestori di Identity
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();

// Registrazione di AuthService
builder.Services.AddScoped<AuthService>();

// Altri servizi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura la pipeline
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