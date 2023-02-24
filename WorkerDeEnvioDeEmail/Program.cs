using WorkerDeEnvioDeEmail;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })// Para funcionar como um serviço do Windows é necessário adicionar Microsoft.Extensions.Hosting.WindowsServices
      //.UseWindowsService
    .Build();

host.Run();
