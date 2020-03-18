using MediatR;
using UserService.Application.UseCases.Users.Request;


namespace UserService.Application.UseCases.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<GetUserDto>
    {
        public int Id { get; set; }

        public GetUserQuery(int id)
        {
            Id = id;
        }
    }
}
