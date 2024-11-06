// See https://aka.ms/new-console-template for more information
using EasyNetQ;
using eProdaja.Modeli.Messages;

Console.WriteLine("Hello, World!");

var bus = RabbitHutch.CreateBus("host=localhost");
await bus.PubSub.SubscribeAsync<ProizvodiActivated>("console_printer", msg =>
{
    Console.WriteLine($"Product activated: {msg.Proizvod.Naziv}");
});
await bus.PubSub.SubscribeAsync<ProizvodiActivated>("console_printer", msg =>
{
    Console.WriteLine($"Product activated 2: {msg.Proizvod.Naziv}");
});
await bus.PubSub.SubscribeAsync<ProizvodiActivated>("mail_sender", msg =>
{
    Console.WriteLine($"Sending email for: {msg.Proizvod.Naziv}");
    //todo send email
});
Console.WriteLine("Listening for messages, press <return> key to close!");
Console.ReadLine();
