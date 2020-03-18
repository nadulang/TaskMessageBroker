using MediatR;

namespace NotificationService.Application.UseCases.Notifications.Queries.GetNotification
{
    public class GetNotificationQuery : IRequest<GetNotificationDto>
    {
        public int Id { get; set; }

        public GetNotificationQuery(int id)
        {
            Id = id;
        }
    }
}
