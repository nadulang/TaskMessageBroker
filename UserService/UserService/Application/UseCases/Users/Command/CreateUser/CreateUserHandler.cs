using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UserService.Application.Models.Query;
using UserService.Application.UseCases.Users.Request;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistences;

namespace UserService.Application.UseCases.Users.Command.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly UsersContext _context;

        public CreateUserHandler(UsersContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var user = new Users_()
            {
                name = request.Data.Attributes.name,
                username = request.Data.Attributes.username,
                email = request.Data.Attributes.email,
                password = request.Data.Attributes.password,
                address = request.Data.Attributes.address
            };

            _context.UsersData.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var user1 = _context.UsersData.First(x => x.username == request.Data.Attributes.username);
            var target = new Target() { Id = user.id, Email_destination = user.email };
            var client = new HttpClient();

            var command = new NotifInput()
            {
                Title = "rabbit-test",
                Message = "this is only testing",
                Type = "email",
                From = 98780,
                Target = new List<Target>() { target }
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
            //var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            //await client.PostAsync("http://localhost:5800/api/notification", );


            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "pakpos", "fanout");

                var body = Encoding.UTF8.GetBytes(jsonObject);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "pakpos", 
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine("Message has sent");
                
                Console.ReadLine();
            
            }
            Console.ReadLine();

            return new UserDto
            {
                message = "a user has been added.",
                success = true
            };
        }
    }
}
