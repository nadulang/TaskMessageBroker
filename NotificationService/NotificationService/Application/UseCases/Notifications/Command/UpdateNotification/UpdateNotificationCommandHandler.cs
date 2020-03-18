using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NotificationService.Application.UseCases.Notifications.Command.Request;
using NotificationService.Infrastructure.Persistences;

namespace NotificationService.Application.UseCases.Notifications.Command.UpdateNotification
{
    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, NotifDto>
    {
        private readonly NotificationContext _context;

        public UpdateNotificationCommandHandler(NotificationContext context)
        {
            _context = context;
        }

        public async Task<NotifDto> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var logs1 = _context.Logs.ToList();

            var find = logs1.Where(x => x.notification_id == request.Data.Attributes.notification_id);
            var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;

            foreach (var k in request.Data.Attributes.target)
            {
                var data = find.First(l => l.target == k.id).id;
                var dataContext = await _context.Logs.FindAsync(data);
                dataContext.read_at = (long)time;
                await _context.SaveChangesAsync();

            }

            return new NotifDto
            {
                message = "Success retreiving data",
                success = true
            };
        }
    }
}
