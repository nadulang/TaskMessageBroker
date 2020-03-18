using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NotificationService.Application.UseCases.Notifications.Command.Request;
using NotificationService.Application.UseCases.Notifications.Request;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Persistences;

namespace NotificationService.Application.UseCases.Notifications.Queries.GetNotification
{
    public class GetNotificationQueryHandler : IRequestHandler<GetNotificationQuery, GetNotificationDto>
    {
        private readonly NotificationContext _context;

        public GetNotificationQueryHandler(NotificationContext context)
        {
            _context = context;
        }

        public async Task<GetNotificationDto> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
        {
            var notif = await _context.Notifications.FirstAsync(x => x.id == request.Id);
            var log = await _context.Logs.Where(x => x.id == request.Id).ToListAsync();

            var logslist = new List<Logs_>();

            foreach (var k in log)
            {
                logslist.Add(new Logs_()
                {
                    notification_id = k.notification_id,
                    from = k.from,
                    read_at = k.read_at,
                    target = k.target
                });

            }

            if (notif == null)
            {
                return null;
            }
            else
            {
                return new GetNotificationDto
                {
                    success = true,
                    message = "Success retreiving data",
                    data = new NotifQueryDto
                    {
                        notification = new Notifications2_()
                        {
                            id = notif.id,
                            title = notif.title,
                            message = notif.message
                        },

                        logs = logslist
                    }
                };
            }
        }
    }
}
