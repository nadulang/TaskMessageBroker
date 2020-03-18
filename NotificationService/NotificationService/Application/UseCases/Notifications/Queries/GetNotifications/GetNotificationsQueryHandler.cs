using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NotificationService.Application.UseCases.Notifications.Request;
using NotificationService.Infrastructure.Persistences;

namespace NotificationService.Application.UseCases.Notifications.Queries.GetNotifications
{
    public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, GetNotificationsDto>
    {
        private readonly NotificationContext _context;

        public GetNotificationsQueryHandler(NotificationContext context)
        {
            _context = context;
        }

        public async Task<GetNotificationsDto> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Notifications.ToListAsync();

            var result = data.Select(e => new Notifications2_
            {
                id = e.id,
                title = e.title,
                message = e.message,
            });

            return new GetNotificationsDto
            {
                message = "Success retrieving data",
                success = true,
                data = result.ToList()
            };
        }
    }
}
