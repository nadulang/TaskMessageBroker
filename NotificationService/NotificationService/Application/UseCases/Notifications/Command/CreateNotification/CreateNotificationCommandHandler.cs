using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NotificationService.Application.UseCases.Notifications.Command.Request;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Persistences;

namespace NotificationService.Application.UseCases.Notifications.Command.CreateNotification
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, NotifDto>
    {
        private readonly NotificationContext _context;

        public CreateNotificationCommandHandler(NotificationContext context)
        {
            _context = context;
        }

        public async Task<NotifDto> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var input = request.Data.Attributes;

            var notdata = new Notifications_
            {
                title = input.Title,
                message = input.Message
            };

            _context.Notifications.Add(notdata);
            await _context.SaveChangesAsync(cancellationToken);

            var ID = await _context.Notifications.ToListAsync();

            foreach (var log in input.Targets)
            {
                _context.Logs.Add(new NotificationLogs_
                {
                    notification_id = ID.Last().id,
                    type = input.Type,
                    from = input.From,
                    target = log.Id,
                    email_destination = log.Email_destination
                });

                await _context.SaveChangesAsync();
            }

            return new NotifDto
            {
                message = "Notification data has been added successfully",
                success = true
            };
        }

        public async Task<List<Users_>> GetUserData()
        {
            var client = new HttpClient();
            var data = await client.GetStringAsync("http://localhost:5400/api/users");
            return JsonConvert.DeserializeObject<List<Users_>>(data);
        }

        public async Task SendMail(string emailfrom, string emailto, string subject, string body)
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("84b015139889ab", "a7eda17f7b7703"),
                EnableSsl = true
            };
            await client.SendMailAsync(emailfrom, emailto, subject, body);
            Console.WriteLine("Email has been sent");
        }
    }
}
