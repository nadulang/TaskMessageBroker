using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Application.UseCases.Users.Request;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistences;

namespace UserService.Application.UseCases.Users.Command.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly UsersContext _context;

        public UpdateUserHandler(UsersContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var user = _context.UsersData.Find(request.Data.Attributes.id);

           
                user.name = request.Data.Attributes.name;
                user.username = request.Data.Attributes.username;
                user.email = request.Data.Attributes.email;
                user.password = request.Data.Attributes.password;
                user.address = request.Data.Attributes.address;

                await _context.SaveChangesAsync(cancellationToken);

                return new UserDto
                {
                    success = true,
                    message = "Data is successfully updated"
                };
        }
    }
}
