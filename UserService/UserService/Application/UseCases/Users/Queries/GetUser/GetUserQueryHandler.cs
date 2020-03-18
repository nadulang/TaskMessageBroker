using MediatR;
using System.Threading.Tasks;
using System.Threading;
using UserService.Infrastructure.Persistences;

namespace UserService.Application.UseCases.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserDto>
    {
        private readonly UsersContext _context;

        public GetUserQueryHandler(UsersContext context)
        {
            _context = context;
        }

        public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.UsersData.FindAsync(request.Id);
            if (result == null)
            {
                return null;
            }
            else
            {
                return new GetUserDto
                {
                    success = true,
                    message = "User succesfully retrieved",
                    Data = result
                };
            }
        }
    }
}
