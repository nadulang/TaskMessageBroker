using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using UserService.Application.Models.Query;
using UserService.Application.UseCases.Users.Request;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistences;

namespace UserService.Application.UseCases.Users.Command.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly UsersContext _context;
        private static readonly HttpClient client = new HttpClient();

        public CreateUserHandler(UsersContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var input = request.Data.Attributes;

            var user = new Users_()
            {
                name = input.name,
                username = input.username,
                email = input.email,
                password = input.password,
                address = input.address
            };

            _context.UsersData.Add(user);
            await _context.SaveChangesAsync();

            var user1 = _context.UsersData.First(x => x.username == request.Data.Attributes.username);
            var target = new Target() { Id = user.id, Email_destination = user.email };

            var command = new NotifInput()
            {
                Title = "hello",
                Message = "this is message body",
                Type = "email",
                From = 123456,
                Targets = new List<Target>() { target }
            };

            var attributes = new Attribute<NotifInput>()
            {
                Attributes = command
            };

            var httpContent = new CommandDTO<NotifInput>()
            {
                Data = attributes
            };

            var jsonObject = JsonConvert.SerializeObject(httpContent);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            await client.PostAsync("http://localhost:1800/api/notification", content);

            return new UserDto()
            {
                message = "Success add a user data",
                success = true
            };
        }
    }
}
