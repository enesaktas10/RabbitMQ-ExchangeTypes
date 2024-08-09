using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://ddmxhagm:7jYQOH-lm2u1Hm3-EoVlwQEsftiuIkLt@chimpanzee.rmq.cloudamqp.com/ddmxhagm");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare(
    exchange: "fanout-exchange-example",
    type: ExchangeType.Fanout);

Console.Write("Kuyruk Adi Gir = ");
string _queueName = Console.ReadLine();

channel.QueueDeclare(
    queue: _queueName,
    exclusive: false);

channel.QueueBind(
    queue:_queueName,
    exchange: "fanout-exchange-example",
    routingKey:string.Empty);

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(
    queue:_queueName,
    consumer:consumer,
    autoAck:true);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.ReadLine();