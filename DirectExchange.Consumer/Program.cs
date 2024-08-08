using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

//Create connection
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://ddmxhagm:7jYQOH-lm2u1Hm3-EoVlwQEsftiuIkLt@chimpanzee.rmq.cloudamqp.com/ddmxhagm");

//Activating the connection and opening a channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//We define the exchange that we define on the publisher side, also on the consumer side.
channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

//Queuing
string queueName = channel.QueueDeclare().QueueName;

//Binding
channel.QueueBind(queue: queueName,exchange: "direct-exchange-example",routingKey: "direct-queue-example");

//Create consumer
EventingBasicConsumer consumer = new (channel);

//Consumer settings
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.ReadLine();
