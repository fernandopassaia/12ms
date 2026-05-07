using Fiscal.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<RabbitMqWorker>();
var host = builder.Build();
host.Run();
