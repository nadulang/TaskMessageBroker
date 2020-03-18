using MediatR;
using UserService.Application.UseCases.Users.Request;

namespace UserService.Application.UseCases.Users.Command.DeleteUser
{
    public class DeleteUserCommand : IRequest<UserDto>
    {
        public int Id { get; set; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}
