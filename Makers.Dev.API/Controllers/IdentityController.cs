using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using AutoMapper;
using Makers.Dev.API.Filters;
using Makers.Dev.Contracts.DTO.Auth;
using Makers.Dev.Contracts.DTO.User;
using Makers.Dev.Contracts.Identity;
using Makers.Dev.Contracts.Services;
using Makers.Dev.Domain.Entities.Auth;
using Makers.Dev.Contracts.DTO.Role;

namespace Makers.Dev.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ServiceErrorExceptionFilter]
public class IdentityController(IMapper mapper, IAuthService authService, IAuthIdentity authIdentity) : ControllerBase
{
  readonly IMapper _mapper = mapper;
  readonly IAuthService _authService = authService;
  readonly IAuthIdentity _authIdentity = authIdentity;

  [AllowAnonymous]
  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthResult))]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest)
  {
    AuthResult auth = await _authIdentity.Register(userRegisterRequest);

    return CreatedAtAction(nameof(Register), auth);
  }

  [AllowAnonymous]
  [HttpPost("login")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResult))]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Login([FromBody] UserLoginRequest userLoginRequest)
  {
    AuthResult auth = await _authIdentity.Login(userLoginRequest);

    return Ok(auth);
  }

  [HttpPut("{userId}")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserRegisterRequest userRegisterRequest)
  {
    UserEntity user = await _authService.FindUserById(userId);
    UserEntity userMap = _mapper.Map(userRegisterRequest, user);
    UserEntity updatedUser = await _authService.UpdateUser(userMap);

    return Ok(_mapper.Map<UserResponse>(updatedUser));
  }

  [HttpDelete("{userId}")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> DeleteUser(Guid userId)
  {
    UserEntity user = await _authService.DeleteUserById(userId);

    return Ok(_mapper.Map<UserResponse>(user));
  }

  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IAsyncEnumerable<RoleResult>))]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public IAsyncEnumerable<RoleResult> GetUsers()
  {
    var users = _authService.GetUsers();

    return _mapper.Map<IAsyncEnumerable<RoleResult>>(users);
  }

  [HttpGet("{userId:guid}")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> FindUserById(Guid userId)
  {
    UserEntity user = await _authService.FindUserById(userId);

    return Ok(_mapper.Map<UserResponse>(user));
  }

  [HttpGet("{usernameOrEmail}")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> FindUserByUsernameOrEmail(string usernameOrEmail)
  {
    UserEntity user = await _authService.FindUserByUsernameOrEmail(usernameOrEmail);

    return Ok(_mapper.Map<UserResponse>(user));
  }
}
