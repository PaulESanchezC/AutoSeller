using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ApplicationUsersModels;
using Models.AuthenticationModels;
using Services.AuthenticationServices;

namespace AutoSellerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IAuthenticationService _authRepository;
    public AccountController(IAuthenticationService authRepository)
    {
        _authRepository = authRepository;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authRepository.LoginAsync(userLoginDto, cancellationToken);
        return result.LoginSuccessful ? Ok(result) : StatusCode(401,result);
    }
    
    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await _authRepository.RegisterAsync(userRegistrationDto, cancellationToken);

        if (result.RegistrationSuccessful)
            return Ok(result);
        return BadRequest(result);
    }
    
    [AllowAnonymous]
    [HttpPost("GetEmailConfirmationTokenAsync/{userEmail}")]
    public async Task<IActionResult> GetEmailConfirmationTokenAsync(string userEmail,
        CancellationToken cancellationToken)
    {
        var result = await _authRepository.GetEmailConfirmationTokenAsync(userEmail, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [AllowAnonymous]
    [HttpPost("EmailConfirmation")]
    public async Task<IActionResult> EmailConfirmation([FromBody] EmailConfirmationDto emailConformationDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authRepository.ValidateEmailConfirmationForUserAsync(emailConformationDto.Email, emailConformationDto.Token, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [AllowAnonymous]
    [HttpPost("GenerateResetPasswordToken/{userEmail}")]
    public async Task<IActionResult> GenerateResetPasswordTokenAsync(string userEmail,
        CancellationToken cancellationToken)
    {
        var result = await _authRepository.GenerateResetPasswordTokenAsync(userEmail, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [AllowAnonymous]
    [HttpPost("ValidateResetPasswordToken/")]
    public async Task<IActionResult> GenerateResetPasswordTokenAsync([FromBody] ResetPasswordTokenValidatorDto resetPasswordTokenValidator, CancellationToken cancellationToken)
    {
        var result = await _authRepository.ValidateResetPasswordTokenAsync(resetPasswordTokenValidator.Email, resetPasswordTokenValidator.ResetPasswordToken, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [AllowAnonymous]
    [HttpPost("ResetPasswordAsync")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody]ResetPasswordDto resetPasswordDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _authRepository.ResetPasswordAsync(resetPasswordDto, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
    
    [AllowAnonymous]
    [HttpPost("LoginWithFacebook/{accessToken}")]
    public async Task<IActionResult> LoginWithFacebook(string accessToken,[FromBody] FacebookRegistrationDto? facebookRegistrationDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var request = await _authRepository.LoginWithFacebookAsync(accessToken, facebookRegistrationDto);
        return StatusCode(request.StatusCode, request);
    }
    
    [HttpPost("SetPasswordAsync")]
    public async Task<IActionResult> SetOAuthUserPassword([FromBody] SetUserPasswordDto setUserPasswordDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var request = await _authRepository.SetPasswordAsync(setUserPasswordDto, cancellationToken);
        return StatusCode(request.StatusCode, request);
    }

    [HttpPut("UpdateApplicationUserProfile")]
    public async Task<IActionResult> UpdateApplicationUserProfile([FromBody] ApplicationUserDto applicationUserDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var request = await _authRepository.UpdateApplicationUserProfileAsync(applicationUserDto, cancellationToken);
        return request.LoginSuccessful ? Ok(request) : BadRequest(request);
    }
}

