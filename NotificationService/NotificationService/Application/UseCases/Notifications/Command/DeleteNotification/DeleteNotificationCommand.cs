using MediatR;
using NotificationService.Application.UseCases.Notifications.Command.Request;

namespace NotificationService.Application.UseCases.Notifications.Command.DeleteNotification
{
    public class DeleteNotificationCommand : IRequest<NotifDto>
    {
        public int Id { get; set; }

        public DeleteNotificationCommand(int id)
        {
            Id = id;
        }
    }
}
