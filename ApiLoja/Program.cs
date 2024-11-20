using System.Security.Claims;
using System.Text;
using ApiLoja;
using ApiLoja.EndPoints;
using ApiLoja.Infra;
using ApiLoja.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

DotEnv.Carregar(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env"));

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddCors();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Config.Instancia.ChavePrivada ?? "")),
        ValidateIssuer = false,
        ValidateAudience = false
    };

    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = ctx =>
        {
            ctx.Request.Cookies.TryGetValue("accessToken", out var accessToken);
            if (!string.IsNullOrEmpty(accessToken))
            {
                ctx.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", policy => policy.RequireRole("admin"))
    .AddPolicy("Cliente", policy => policy.RequireRole("cliente"))
    .AddPolicy("AdminOuCliente", policy => policy.RequireClaim(ClaimTypes.Role, "admin", "cliente"));

builder.Services.AddSingleton<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.AdicionarClienteEndPoints();
app.AdicionarProdutoEndPoints();
app.AdicionarUsuarioEndPoints();
app.AdicionarLoginEndPoints();

app.UseCors(builder => builder
    .WithOrigins("http://localhost:3000")
   .AllowAnyMethod()
   .AllowAnyHeader()
   .AllowCredentials()
);

app.UseAuthentication();
app.UseAuthorization();

app.Run();