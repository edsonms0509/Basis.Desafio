using Basis.CadastroLivros.Data.Repositories;
using Basis.CadastroLivros.Data.Repositories.Interfaces;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IAssuntoRepository, AssuntoRepository>();
builder.Services.AddScoped<ICanalDeVendaRepository, CanalVendaRepository>();
builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Add defined CORS
var path = builder.Configuration.GetSection("CORS:Path").Value ?? string.Empty;

app.UseCors(policy =>
    policy.WithOrigins(path)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithHeaders(HeaderNames.ContentType)
);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
