using System.Collections.Generic;
using NotificationService.Application.Models.Query;
using NotificationService.Application.UseCases.Notifications.Request;

namespace NotificationService.Application.UseCases.Notifications.Queries.GetAll
{
    public class GetAllDto : BaseDto
    {
        public List<NotifQueryDto> data { get; set; }
    }
}
