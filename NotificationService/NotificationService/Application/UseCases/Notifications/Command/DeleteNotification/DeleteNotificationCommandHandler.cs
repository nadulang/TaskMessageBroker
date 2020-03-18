using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NotificationService.Application.UseCases.Notifications.Command.Request;
using NotificationService.Infrastructure.Persistences;

namespace NotificationService.Application.UseCases.Notifications.Command.DeleteNotification
{
    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, NotifDto>
    {
        private readonly NotificationContext _context;

        public DeleteNotificationCommandHandler(NotificationContext context)
        {
            _context = context;
        }

        public async Task<NotifDto> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var deletenotif = await _context.Notifications.FindAsync(request.Id);
            var deletelog = _context.Logs.Where(e => e.notification_id == request.Id);

            if (deletenotif == null && deletelog == null)
            {
                return new NotifDto
                {
                    message = "Not Found",
                    success = false
                };
            }

            else
            {
                _context.Notifications.Remove(deletenotif);
                await _context.SaveChangesAsync(cancellationToken);


                _context.Logs.RemoveRange(deletelog);
                await _context.SaveChangesAsync();

                return new NotifDto
                {
                    message = "notification has been deleted",
                    success = true
                };
            }

        }
    }
}
