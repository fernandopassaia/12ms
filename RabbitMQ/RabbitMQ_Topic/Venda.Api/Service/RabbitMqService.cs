using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Venda.Api.Models;

namespace Venda.Api.Services;

public class RabbitMqService
{
    private IConnection? _connection;
    private IChannel? _channel;
    private const string EXCHANGE_NAME = "venda.exchange";

    public async Task InicializarAsync()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.ExchangeDeclareAsync(
            exchange: EXCHANGE_NAME,
            type: ExchangeType.Topic, // nota: No Fanout todo mundo que ouvir a Exchange recebe a mensagem
            durable: true
        );
    }

    public async Task PublicarEventoAsync(
    OrdemDeVenda venda,
    string routingKey)
    {
        if (_channel is null)
            throw new Exception("RabbitMQ não inicializado");

        var json = JsonSerializer.Serialize(venda);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties
        {
            Persistent = true
        };

        await _channel.BasicPublishAsync(
            exchange: EXCHANGE_NAME,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: properties,
            body: body
        );

        Console.WriteLine($"📤 Evento publicado: {routingKey}");
    }
}