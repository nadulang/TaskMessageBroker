using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.UseCases.Notifications.Command.CreateNotification;
using NotificationService.Application.UseCases.Notifications.Command.DeleteNotification;
using NotificationService.Application.UseCases.Notifications.Command.UpdateNotification;
using NotificationService.Application.UseCases.Notifications.Queries.GetAll;
using NotificationService.Application.UseCases.Notifications.Queries.GetNotification;
using NotificationService.Application.UseCases.Notifications.Queries.GetNotifications;

namespace NotificationService.Presenter.Controller
{
    [ApiController]
    [Route("api/notification")]

    public class NotificationsController : ControllerBase
    {
        private IMediator _mediatr;

        public NotificationsController(IMediator mediatr)
        {
            _mediatr = mediatr;

        }

        [HttpGet]
        public async Task<ActionResult> Get(string include)
        {
            if (include == "logs")
            {
                var notif = new GetAllQuery();
                return Ok(await _mediatr.Send(notif));
            }
            else
            {
                var notif = new GetNotificationsQuery();

                return Ok(await _mediatr.Send(notif));
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var notif = new GetNotificationQuery(id);

            return Ok(await _mediatr.Send(notif));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateNotificationCommand data)
        {
            var result = await _mediatr.Send(data);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var notif = new DeleteNotificationCommand(id);
            var result = await _mediatr.Send(notif);
            return result != null ? (IActionResult)Ok(new { Message = "success" }) : NotFound(new { Message = "Notification not found" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateNotificationCommand data)
        {
            data.Data.Attributes.notification_id = id;
            var result = await _mediatr.Send(data);
            return Ok(result);
        }

    }
}
