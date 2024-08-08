using System.Text;
using RabbitMQ.Client;

//Create connection
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://ddmxhagm:7jYQOH-lm2u1Hm3-EoVlwQEsftiuIkLt@chimpanzee.rmq.cloudamqp.com/ddmxhagm");

//Activating the connection and opening a channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Creating an exchange and define exchange type
channel.ExchangeDeclare(exchange:"direct-exchange-example",type:ExchangeType.Direct);

while (true)
{
    Console.WriteLine("Mesaj gir = ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

    //We publish the messages we receive from the consoleee
    channel.BasicPublish(exchange: "direct-exchange-example",routingKey:"direct-queue-example",body:byteMessage);

}

Console.ReadLine();
