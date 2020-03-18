using MediatR;
using UserService.Application.Models.Query;
using UserService.Application.UseCases.Users.Request;
using UserService.Domain.Entities;

namespace UserService.Application.UseCases.Users.Command.UpdateUser
{
    public class UpdateUserCommand : CommandDTO<Users_>, IRequest<UserDto>
    {
        
    }
}
