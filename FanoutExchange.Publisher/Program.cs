using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://ddmxhagm:7jYQOH-lm2u1Hm3-EoVlwQEsftiuIkLt@chimpanzee.rmq.cloudamqp.com/ddmxhagm");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange:"fanout-exchange-example",
    type:ExchangeType.Fanout);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");

    channel.BasicPublish(
        exchange: "fanout-exchange-example",
        routingKey:string.Empty,
        body:message);

}

Console.Read();