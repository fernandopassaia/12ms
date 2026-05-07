using Venda.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddOpenApi();
builder.Services.AddControllers(); // 👈 IMPORTANTE
builder.Services.AddSingleton<RabbitMqService>();

var app = builder.Build();

// Inicializa RabbitMQ
var rabbit = app.Services.GetRequiredService<RabbitMqService>();
await rabbit.InicializarAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers(); // 👈 IMPORTANTE

app.Run();