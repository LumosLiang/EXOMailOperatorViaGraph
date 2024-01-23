// See https://aka.ms/new-console-template for more information
using EXOMailOperatorViaGraph;
using EXOMailOperatorViaGraph.Service;

Console.WriteLine("Start Sending Email!");

var settings = Settings.LoadSettings();

// Initialize Graph
GraphHelper.InitializeGraphForAppOnlyAuth(settings);
EXOMailOperator Operator = new();
int count = 0;

while (true)
{
    await Operator.CopyMailAsync();
    Console.WriteLine("Copying email {0}", count++);
    // if (count > 100)
    //     break;

    Thread.Sleep(5000);
}

