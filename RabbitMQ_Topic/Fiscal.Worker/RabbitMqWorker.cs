using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Fiscal.Worker.Models;

namespace Fiscal.Worker;

public class RabbitMqWorker : BackgroundService
{
    private IConnection? _connection;
    private IChannel? _channel;

    private const string EXCHANGE_NAME = "venda.exchange";
    private const string QUEUE_NAME = "fiscal.queue";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        // Garante exchange (mesma do producer)
        await _channel.ExchangeDeclareAsync(
            exchange: EXCHANGE_NAME,
            type: ExchangeType.Topic,
            durable: true
        );

        // Cria fila
        await _channel.QueueDeclareAsync(
            queue: QUEUE_NAME,
            durable: true,
            exclusive: false,
            autoDelete: false
        );

        // Faz o binding
        await _channel.QueueBindAsync(
            queue: QUEUE_NAME,
            exchange: EXCHANGE_NAME,
            routingKey: "venda.*"
        );

        Console.WriteLine("📦 Fiscal Worker aguardando mensagens...");

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (sender, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var ordem = JsonSerializer.Deserialize<OrdemDeVenda>(json);

                switch (ea.RoutingKey)
                {
                    case "venda.criada":
                        Console.WriteLine("📦 Emitindo NFe...");
                        break;

                    case "venda.excluida":
                        Console.WriteLine("🔄 Estornando NFe...");
                        break;
                }
                Console.WriteLine($"Cliente: {ordem?.NomeCliente}");
                Console.WriteLine($"Itens: {ordem?.Itens.Count}");

                // Simula processamento
                await Task.Delay(5000);

                // ACK manual
                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro: {ex.Message}");

                // NACK (não requeue por enquanto)
                await _channel.BasicNackAsync(ea.DeliveryTag, false, false);
            }
        };

        await _channel.BasicConsumeAsync(
            queue: QUEUE_NAME,
            autoAck: false,
            consumer: consumer
        );
    }
}