using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.UseCases.Users.Command.CreateUser;
using UserService.Application.UseCases.Users.Command.DeleteUser;
using UserService.Application.UseCases.Users.Command.UpdateUser;
using UserService.Application.UseCases.Users.Queries.GetUser;
using UserService.Application.UseCases.Users.Queries.GetUsers;

namespace UserService.Presenter.Controller
{
    [ApiController]
    [Route("api/users")]

    public class UserController : ControllerBase
    {
        private IMediator _mediatr;

        public UserController(IMediator mediatr)
        {
            _mediatr = mediatr;

        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _mediatr.Send(new GetUsersQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {

            var command = new GetUserQuery(id);
            var result = await _mediatr.Send(command);
            return result != null ? (ActionResult)Ok(new { Message = "success", data = result }) : NotFound(new { Message = "not found" });

        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommand data)
        {
            var result = await _mediatr.Send(data);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteUserCommand(id);
            var result = await _mediatr.Send(command);

            return command != null ? (IActionResult)Ok(new { Message = "deleted" }) : NotFound(new { Message = "not found" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateUserCommand req)
        {
         
            req.Data.Attributes.id = id;
            var result = await _mediatr.Send(req);
            return Ok(result);

        }
    }
}
