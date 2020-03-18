using UserService.Application.Models.Query;
using UserService.Domain.Entities;

namespace UserService.Application.UseCases.Users.Queries.GetUser
{
    public class GetUserDto : BaseDto
    {
        public Users_ Data { get; set; }
    }
}
