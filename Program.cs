// See https://aka.ms/new-console-template for more information
using SimpleMailSender;

Console.WriteLine("Start Sending Email!");

var settings = Settings.LoadSettings();

// Initialize Graph
GraphHelper.InitializeGraphForAppOnlyAuth(settings);

var sender = new SimpleEmailSender();
int count = 0;

while (true)
{
    await sender.SendMailAsync();
    Console.WriteLine("Sending email {0}", count++);

    if (count > 100)
        break;

    Thread.Sleep(10000);
}

