using NotificationService.Application.Models.Query;
using NotificationService.Application.UseCases.Notifications.Request;

namespace NotificationService.Application.UseCases.Notifications.Queries.GetNotification
{
    public class GetNotificationDto : BaseDto
    {
        public NotifQueryDto data { get; set; }
    }
}
