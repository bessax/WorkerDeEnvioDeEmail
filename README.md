# 📧 Worker de Envio de E-mail
 
Projeto de exemplo em **.NET 7** que mostra como criar um **Worker Service** (serviço em segundo plano) capaz de enviar e-mails automaticamente, em intervalos regulares, usando o **SMTP do Gmail**.
 
É um ótimo ponto de partida para quem quer aprender sobre `BackgroundService`, hospedagem genérica (`Generic Host`) e envio de e-mails com `System.Net.Mail`.
 
---
 
## ✨ O que o projeto faz
 
- Executa como um processo contínuo em background (`BackgroundService`).
- A cada **1 minuto**, envia um e-mail de teste via `smtp.gmail.com` (porta `587`, SSL/TLS habilitado).
- Registra no log a mensagem `E-mail enviado com sucesso!` a cada envio.
- Para de forma limpa quando o `CancellationToken` é acionado (Ctrl+C ou parada do serviço).
- Pode ser transformado em **Serviço do Windows** (veja a seção abaixo).
## 🧰 Tecnologias
 
| Tecnologia | Uso |
|---|---|
| .NET 7 (`Microsoft.NET.Sdk.Worker`) | Plataforma e template do Worker Service |
| `Microsoft.Extensions.Hosting` 7.0.0 | Host genérico, DI e logging |
| `System.Net.Mail` (`SmtpClient`, `MailMessage`) | Envio de e-mails via SMTP |
| Gmail SMTP | Servidor de e-mail utilizado no exemplo |
 
## 📁 Estrutura do repositório
 
```
WorkerDeEnvioDeEmail/
├── WorkerDeEnvioDeEmail.sln
└── WorkerDeEnvioDeEmail/
    ├── Program.cs                      # Configuração e inicialização do host
    ├── Worker.cs                       # Lógica do serviço: loop de envio de e-mails
    ├── appsettings.json                # Configurações de logging
    ├── appsettings.Development.json    # Configurações para ambiente de desenvolvimento
    ├── Properties/launchSettings.json  # Perfil de execução local
    └── WorkerDeEnvioDeEmail.csproj
```
 
## ✅ Pré-requisitos
 
- [.NET SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0) ou superior
- Uma conta Gmail com **verificação em duas etapas** ativada
- Uma **senha de app** do Google (veja abaixo)
## 🔑 Configurando o Gmail
 
O Gmail não aceita mais a senha normal da conta para aplicações externas. Você precisa gerar uma **senha de app**:
 
1. Ative a verificação em duas etapas na sua conta Google.
2. Acesse <https://myaccount.google.com/apppasswords>.
3. Crie uma senha de app e copie o código de 16 caracteres gerado.
4. Use esse código no lugar da senha no `Worker.cs`.
## 🚀 Como executar
 
```bash
# 1. Clone o repositório
git clone https://github.com/<seu-usuario>/WorkerDeEnvioDeEmail.git
cd WorkerDeEnvioDeEmail
 
# 2. Ajuste e-mail e senha de app no arquivo Worker.cs (veja abaixo)
 
# 3. Restaure as dependências e execute
cd WorkerDeEnvioDeEmail
dotnet restore
dotnet run
```
 
No arquivo `WorkerDeEnvioDeEmail/Worker.cs`, substitua os valores de exemplo:
 
```csharp
Credentials = new NetworkCredential("meu_email@gmail.com", "minha_senha"), // e-mail e senha de app
...
From = new MailAddress("meu_email@gmail.com"),                              // remetente
...
mailMessage.To.Add("destinatario@exemplo.com");                              // destinatário
```
 
Se tudo estiver certo, você verá no console algo como:
 
```
info: WorkerDeEnvioDeEmail.Worker[0]
      E-mail enviado com sucesso!
```
 
## 🪟 Executando como Serviço do Windows
 
O `Program.cs` já traz o caminho indicado em comentário. Para rodar como serviço:
 
1. Adicione o pacote:
```bash
dotnet add package Microsoft.Extensions.Hosting.WindowsServices
```
2. Habilite no `Program.cs`:
```csharp
IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();
```
3. Publique e registre o serviço:
```bash
dotnet publish -c Release -o ./publish
sc create WorkerDeEnvioDeEmail binPath="C:\caminho\para\publish\WorkerDeEnvioDeEmail.exe"
sc start WorkerDeEnvioDeEmail
```
 
## ⚠️ Segurança: nunca suba credenciais para o GitHub
 
No exemplo, e-mail e senha estão diretamente no código **apenas para fins didáticos**. Em um projeto real, não faça commit de senhas. Use **User Secrets** no desenvolvimento (o projeto já possui um `UserSecretsId`):
 
```bash
dotnet user-secrets set "Smtp:User" "seu_email@gmail.com"
dotnet user-secrets set "Smtp:Password" "sua_senha_de_app"
```
 
e leia os valores via `IConfiguration` (em produção, use variáveis de ambiente ou um cofre de segredos, como o Azure Key Vault).
 
## 🛣️ Ideias de evolução
 
- [ ] Mover host, porta, remetente, destinatário e intervalo para o `appsettings.json` (com o padrão *Options*).
- [ ] Ler credenciais de User Secrets / variáveis de ambiente.
- [ ] Tratar exceções com `try/catch` para que uma falha de envio não derrube o serviço.
- [ ] Liberar o `SmtpClient`/`MailMessage` com `using` e usar `SendMailAsync`.
- [ ] Extrair o envio para uma interface (`IEmailSender`) e injetar via DI, facilitando testes.
- [ ] Buscar e-mails a enviar de uma fila ou banco de dados, em vez de uma mensagem fixa.
- [ ] Adicionar suporte a Docker.
## 🤝 Contribuindo
 
Contribuições são muito bem-vindas! Abra uma *issue* para discutir ideias ou envie um *pull request*.
 
## 📄 Licença
 
Defina aqui a licença do projeto (por exemplo, [MIT](https://opensource.org/licenses/MIT)).
 
---
 
Feito com 💙 para estudo de **.NET Worker Services**.
 
