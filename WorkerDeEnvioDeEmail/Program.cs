using WorkerDeEnvioDeEmail;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })// Para funcionar como um servi�o do Windows � necess�rio adicionar Microsoft.Extensions.Hosting.WindowsServices
      //.UseWindowsService
    .Build();

host.Run();
