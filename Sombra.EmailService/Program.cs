using System;
using System.IO;
using System.Reflection;
using System.Threading;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Messaging.Infrastructure;

namespace Sombra.EmailService
{
    class Program
    {
        private static string _rabbitMqConnectionString;
        private static EmailConfiguration _emailConfiguration;

        static void Main(string[] args)
        {
            Console.WriteLine("EmailService started..");

            SetupConfiguration();
            var serviceProvider = MessagingInstaller.Run(
                Assembly.GetExecutingAssembly(),
                _rabbitMqConnectionString,
                services => services.AddTransient(sp =>
                {
                    var client = new SmtpClient();
                    client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                    return client;
                }));

            Thread.Sleep(Timeout.Infinite);
        }

        private static void SetupConfiguration()
        {
            var isContainerized = Environment.GetEnvironmentVariable("CONTAINER_TYPE") != null;
            if (isContainerized)
            {
                _rabbitMqConnectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING");
                _emailConfiguration = new EmailConfiguration
                {
                    SmtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER"),
                    SmtpPort = Convert.ToInt32(Environment.GetEnvironmentVariable("SMTP_PORT")),
                    SmtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME"),
                    SmtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD")
                };
            }
            else
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

                _rabbitMqConnectionString = config["RABBITMQ_CONNECTIONSTRING"];
                var emailConfig = config.GetSection("EmailConfiguration");
                _emailConfiguration = new EmailConfiguration
                {
                    SmtpServer = emailConfig["SmtpServer"],
                    SmtpPort = Convert.ToInt32(emailConfig["SmtpPort"]),
                    SmtpUsername = emailConfig["SmtpUsername"],
                    SmtpPassword = emailConfig["SmtpPassword"]
                };
            }
        }
    }
}
