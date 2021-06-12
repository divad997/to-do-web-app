using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using ToDoApi.ServiceOptions;
using ToDoCore.Models;
using ToDoInfrastructure;

namespace ToDoApi.Services
{
    public class TimedReminderService : IHostedService
    {
        private readonly IServiceProvider _provider;
        private readonly IOptionsMonitor<TimedReminderOptions> _options;
        private Timer _timer;

        public TimedReminderService(IOptionsMonitor<TimedReminderOptions> options, IServiceProvider provider)
        {
            _provider = provider;
            _options = options;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
             _timer = new Timer(DoWork, null, 0, Int32.Parse(_options.CurrentValue.TimerInterval));
        }

        private void DoWork(object state)
        {
            using (var scope = _provider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();

                var lists = dbContext.ToDoLists.Where(x => x.DueDate > DateTime.Now && !x.Reminded).ToList();

                foreach (ToDoList list in lists)
                {
                    if (SendMailAsync(list).Status == TaskStatus.RanToCompletion)
                    {
                        dbContext.ToDoLists.FindAsync(list.Id).Result.Reminded = true;
                    }
                }
            }
        }

        public async Task SendMailAsync(ToDoList list)
        {
            try
            {
                var message = new MailMessage();

                message.To.Add(new MailAddress(list.Email));
                message.From = new MailAddress("office@novalite.rs");
                message.Subject = "You should do your ToDo!";
                message.Body = "Please check your ToDo list at: " + "http://localhost:4200/to-do-list/" + list.Id;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = _options.CurrentValue.Credentials.Split('/')[0],
                        Password = _options.CurrentValue.Credentials.Split('/')[1],
                    };

                    smtp.Credentials = credential;
                    smtp.Host = "smtp.sendgrid.net";
                    smtp.Port = 465;
                    smtp.EnableSsl = true;

                    await smtp.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Dispose();
            return Task.CompletedTask;
        }
    }
}
