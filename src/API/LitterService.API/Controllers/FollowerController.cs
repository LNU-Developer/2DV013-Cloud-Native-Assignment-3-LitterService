using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using LitterService.Application.Features.Followings.Commands.CreateFollowing;
using LitterService.Application.Features.Followings.Commands.DeleteFollowing;
using LitterService.Application.Features.Followings.Queries.GetFollowingsByUserId;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LitterService.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class FollowerController : ControllerBase
    {
        private readonly IMediator mediator;
        public FollowerController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("/api/Followings/{userId}")]
        public async Task<IActionResult> FollowingsByUserId([FromRoute] Guid userId)
        {
            return Ok(await mediator.Send(new GetFollowingsByUserIdQuery(userId)));
        }

        [HttpPost("/api/Follow/{followedUserId}")]
        public async Task<IActionResult> FollowUser([FromRoute] Guid followedUserId)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var followerId = GetUserId(token);
            await mediator.Send(new CreateFollowingCommand(followerId, followedUserId));
            return Ok();
        }
        [HttpDelete("/api/Follow/{followedUserId}")]
        public async Task<IActionResult> UnfollowUser([FromRoute] Guid followedUserId)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var followerId = GetUserId(token);
            await mediator.Send(new DeleteFollowingCommand(followerId, followedUserId));
            return Ok();
        }

        private static Guid GetUserId(string accessToken)
        {
            var securityTokenHandler = new JwtSecurityTokenHandler();
            if (securityTokenHandler.CanReadToken(accessToken))
            {
                var decriptedToken = securityTokenHandler.ReadJwtToken(accessToken);
                var claims = decriptedToken.Claims;
                return Guid.Parse(claims.Where(c => c.Type == "uid").FirstOrDefault().Value);
            }
            return Guid.NewGuid();
        }
    }
}
