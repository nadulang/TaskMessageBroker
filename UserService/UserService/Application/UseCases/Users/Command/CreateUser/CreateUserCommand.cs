using System.Collections.Generic;
using MediatR;
using UserService.Application.Models.Query;
using UserService.Application.UseCases.Users.Request;
using UserService.Domain.Entities;

namespace UserService.Application.UseCases.Users.Command.CreateUser
{
    public class CreateUserCommand : CommandDTO<Users_>, IRequest<UserDto>
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
