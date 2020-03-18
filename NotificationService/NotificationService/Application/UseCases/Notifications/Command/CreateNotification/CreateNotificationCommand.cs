using System.Collections.Generic;
using MediatR;
using NotificationService.Application.Models.Query;
using NotificationService.Application.UseCases.Notifications.Command.Request;

namespace NotificationService.Application.UseCases.Notifications.Command.CreateNotification
{
    public class CreateNotificationCommand : CommandDTO<NotifInput>, IRequest<NotifDto>
    {
        
    }

    public class NotifInput
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public int From { get; set; }
        public List<Target> Targets { get; set; }
    }

    public class Target
    {
        public int Id { get; set; }
        public string Email_destination { get; set; }
    }
}
