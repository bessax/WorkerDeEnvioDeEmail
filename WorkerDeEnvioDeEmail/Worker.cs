using System.Net.Mail;
using System.Net;

namespace WorkerDeEnvioDeEmail
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Configurações do servidor SMTP
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("meu_email@gmail.com", "minha_senha"),
                    EnableSsl = true,
                    UseDefaultCredentials= false,
                   
                };

                // Configurações do e-mail
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("meu_email@gmail.com"),
                    Subject = "[Worker Service - Serviço que manda e-mail]",
                    Body = "Super teste!!!!!",
                };
                mailMessage.To.Add("andrebessax@gmail.com");

                // Enviar o e-mail
                smtpClient.Send(mailMessage);

                _logger.LogInformation("E-mail enviado com sucesso!");

                await Task.Delay(60000, stoppingToken); // Espera 1 minuto antes de enviar outro e-mail
            }
        }
    }
}