using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using LitterService.Application.Features.Lits.Commands.CreateLit;
using LitterService.Application.Features.Lits.Queries.GetLitsByUserId;
using LitterService.Application.Features.Lits.Queries.GetOwnAndFollowedLits;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LitterService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LitController : ControllerBase
    {
        private readonly IMediator mediator;
        public LitController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("/api/Lits/{userId}")]
        public async Task<IActionResult> GetLitsByUserId([FromRoute] Guid userId)
        {
            return Ok(await mediator.Send(new GetLitsByUserIdQuery(userId)));
        }

        [HttpGet("/api/Lits/")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetOwnAndFollowerLits()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return Ok(await mediator.Send(new GetOwnAndFollowedLitsQuery(GetUserId(token))));
        }

        [HttpPost()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SubmitLit(string message)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            await mediator.Send(new CreateLitCommand(
                GetUserId(token),
                message
            ));
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
