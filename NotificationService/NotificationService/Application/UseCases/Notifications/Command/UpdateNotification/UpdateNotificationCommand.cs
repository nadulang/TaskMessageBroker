using System;
using System.Collections.Generic;
using MediatR;
using NotificationService.Application.Models.Query;
using NotificationService.Application.UseCases.Notifications.Command.Request;

namespace NotificationService.Application.UseCases.Notifications.Command.UpdateNotification
{
    public class UpdateNotificationCommand : CommandDTO<UpdateNotification>, IRequest<NotifDto>
    {
        public UpdateNotificationCommand()
        {
        }
    }

    public class UpdateNotification
    {
        public int notification_id { get; set; }
        public DateTime read_at { get; set; }
        public List<Target1> target { get; set; }
    }

    public class Target1
    {
        public int id { get; set; }
    }
}
