// container
var container = ConsoleApp.CreateContainer(services =>
{
    //services.AddTransient<ISomeService, SomeService>();
});

// demo
ConsoleApp.ShowDemo(container);
