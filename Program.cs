using APIShortLink.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//setando nas politicas de acesso que pode ser acessado de qualquer origem, e pode chamar qualquer método.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    );
});

var connectionString = builder.Configuration.GetConnectionString("DevEncurtaUrl");


builder.Services.AddDbContext<DevEncurtaUrlDbContext>(o=>o.UseInMemoryDatabase("DevEncurtaDb"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DevEncurtaUrl.Api",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Lisandra Gomes", Email = "Lisandragomes53@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/lisandra-gomes-877285111/")
        }
    });

    var xmlFile = "APIShortLink.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
