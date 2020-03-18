using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Application.UseCases.Users.Request;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistences;

namespace UserService.Application.UseCases.Users.Command.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, UserDto>
    {
        private readonly UsersContext _context;

        public DeleteUserHandler(UsersContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var delete = await _context.UsersData.FindAsync(request.Id);

            if (delete == null)
            {
                return null;
            }

            else
            {
                _context.UsersData.Remove(delete);
                await _context.SaveChangesAsync(cancellationToken);

                return new UserDto
                {
                    success = true,
                    message = "Successfully deleted a user"
                };

            }
        }
    }
}
