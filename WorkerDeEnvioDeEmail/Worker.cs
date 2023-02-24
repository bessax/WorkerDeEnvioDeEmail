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

        //par usar o gmail acesse o link https://myaccount.google.com/apppasswords?rapt=AEjHL4OUhJU8Lh-WPgNiYF2pkq3KpWyB5K60lY3N0Q_HaxZaaVGRYjdMfOa4nauClf0Xj0dR5DaZkJp5_puZ7UKU5ZUIkBMJyw
        // e crie uma senha para usar no c�digo.
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Configura��es do servidor SMTP
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("meu_email@gmail.com", "minha_senha"),
                    EnableSsl = true,
                    UseDefaultCredentials= false,
                   
                };

                // Configura��es do e-mail
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("meu_email@gmail.com"),
                    Subject = "[Worker Service - Servi�o que manda e-mail]",
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