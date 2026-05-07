using ProductMongoRedisGraphQlSerilogReact.Configurations;
using ProductMongoRedisGraphQlSerilogReact.Repositories;
using ProductMongoRedisGraphQlSerilogReact.Services;
using ProductMongoRedisGraphQlSerilogReact.Interfaces;
using StackExchange.Redis;
using Serilog;
using ProductMongoRedisGraphQlSerilogReact.GraphQL;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// adiciona os Resolvers do GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<ProductQuery>()
    .AddMutationType<ProductMutation>();

builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect("localhost:6379"));

var app = builder.Build();

app.UseSwagger();

app.MapGraphQL();

app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();