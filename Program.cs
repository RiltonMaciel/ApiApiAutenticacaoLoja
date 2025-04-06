using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ApiAutenticacao.Data;
using ApiAutenticacao.Services;
using ApiAutenticacao.Services.Interfaces;
using ApiAutenticacao.Services.Produto;
using ApiAutenticacao.Services.Carrinho; // ✅ Adicionado para injeção de dependência


var builder = WebApplication.CreateBuilder(args);

// 🔹 1. Controllers
builder.Services.AddControllers();

// 🔹 2. Configuração do Swagger (interface de documentação interativa)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Autenticação e Loja",
        Version = "v1"
    });

    // 🛡️ Permite enviar o token JWT direto pelo Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// 🔹 3. Conexão com o SQL Server (configure sua connection string no appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 4. Injeção de dependência dos serviços (injeção automática onde necessário)
builder.Services.AddScoped<IAuthService, AuthService>(); // Serviço de autenticação
builder.Services.AddScoped<IProdutoService, ProdutoService>(); // Serviço de produtos
builder.Services.AddScoped<ICarrinhoService, CarrinhoService>(); // ✅ Serviço de carrinho

// 🔹 5. Autenticação com JWT (token usado para acessar endpoints protegidos)
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // Ignora validação do emissor
        ValidateAudience = false, // Ignora validação de público
        ValidateLifetime = true, // Verifica expiração do token
        ValidateIssuerSigningKey = true, // Valida assinatura do token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)) // Chave de assinatura
    };
});

var app = builder.Build();

// 🔹 6. Aplica as migrations automaticamente ao iniciar (cria ou atualiza tabelas)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// 🔹 7. Configura o Swagger para aparecer direto na raiz da aplicação (https://localhost:xxxx/)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Autenticação e Loja V1");
        c.RoutePrefix = string.Empty;
    });
}

// 🔹 8. Middlewares HTTP da pipeline
app.UseHttpsRedirection();       // Redireciona para HTTPS
app.UseAuthentication();         // Verifica e valida o token JWT
app.UseAuthorization();          // Aplica as regras de acesso com base no token
app.MapControllers();            // Mapeia todos os controllers/endpoints

// 🔹 9. Inicia a aplicação
app.Run();
