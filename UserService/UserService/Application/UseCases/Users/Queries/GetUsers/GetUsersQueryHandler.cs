using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Models.Query;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistences;

namespace UserService.Application.UseCases.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersDto>
    {
        private readonly UsersContext _context;

        public GetUsersQueryHandler(UsersContext context)
        {
            _context = context;
        }

        public async Task<GetUsersDto> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.UsersData.ToListAsync();

            var result = data.Select(e => new Users_
            {
                id = e.id,
                name = e.name,
                username = e.username,
                email = e.email,
                password = e.password,
                address = e.address
            });

            return new GetUsersDto
            {
                message = "Success retrieving data",
                success = true,
                data = result.ToList()
            };
        }
    }
}
