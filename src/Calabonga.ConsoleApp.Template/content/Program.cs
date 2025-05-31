// container

using Calabonga.ConsoleApp;

var container = ConsoleApp.CreateContainer(services =>
{
    //services.AddTransient<ISomeService, SomeService>();
});

// demo
ConsoleApp.ShowDemo(container);
