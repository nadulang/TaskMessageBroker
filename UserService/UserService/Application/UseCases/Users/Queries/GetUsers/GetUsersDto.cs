using System.Collections.Generic;
using UserService.Application.Models.Query;
using UserService.Domain.Entities;

namespace UserService.Application.UseCases.Users.Queries.GetUsers
{
    public class GetUsersDto : BaseDto
    {
        public List<Users_> data { get; set; }
    }
}
