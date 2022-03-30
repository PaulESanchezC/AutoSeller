using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Models.ApplicationUserModels;
using Models.AuthenticationModels;
using Models.PagesViewModels;
using Services.AuthenticationService;
using Services.AuthenticationService.Templates;
using Services.EmailServices;
using Services.EmailServices.EmailTemplates;
using Services.JwtBearerTokens;
using Services.StaticData.Roles;
using Services.StaticData.SweetAlertTemplates;
using Services.TutorialServices;

namespace AutoSellerClientWebApplication.Controllers;

public class LoginController : Controller
{
    private readonly ITutorialService _tutorial;
    private readonly IAuthenticationServices _authentication;
    private readonly IMailJetEmailSender _mailJet;
    public LoginController(IAuthenticationServices authentication, IMailJetEmailSender mailJet, ITutorialService tutorial)
    {
        _authentication = authentication;
        _mailJet = mailJet;
        _tutorial = tutorial;
    }

    #region Login

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (HttpContext.User.Identity.IsAuthenticated)
            RedirectToAction("Dashboard", "User");

        if (string.IsNullOrEmpty(returnUrl))
            return View(new ApplicationUserLoginVm());

        var loginVm = new ApplicationUserLoginVm();
        loginVm.ReturnUrl = returnUrl;
        return View(loginVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginAsync(ApplicationUserLoginVm appUserLoginVm)
    {
        if (!ModelState.IsValid)
            return View(nameof(Login), appUserLoginVm);
        
        if (appUserLoginVm.ExternalLogin != "local"
            && appUserLoginVm.AccessToken != "none")
        {
            var externalLoginVm = new ExternalLoginVm
            {
                AccessToken = appUserLoginVm.AccessToken,
                LoginProvider = appUserLoginVm.ExternalLogin,
            };
            if (!string.IsNullOrEmpty(appUserLoginVm.ReturnUrl))
                externalLoginVm.ReturnUrl = appUserLoginVm.ReturnUrl;
            return RedirectToAction("ExternalLogin", externalLoginVm);
        }

        var request = await _authentication.LoginAsync(appUserLoginVm);
        if (!request.LoginSuccessful)
        {
            TempData["Login"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Error;
            TempData["Message"] = SweetAlertHelper.Messages.InvalidLogin;
            return View("login",appUserLoginVm);
        }
        await SetLoginWithHttpCookieAsync(request.ApplicationUserDto, request.JWToken);
        TempData["Login"] = true;
        TempData["Type"] = SweetAlertHelper.Types.Success;
        TempData["Message"] = SweetAlertHelper.Messages.ValidLogin;
        if (string.IsNullOrEmpty(appUserLoginVm.ReturnUrl))
            return RedirectToAction("Dashboard", "User");
        return Redirect(appUserLoginVm.ReturnUrl);
    }

    [HttpGet]
    public async Task<IActionResult> ExternalLogin(ExternalLoginVm externalLoginVm)
    {
        switch (externalLoginVm.LoginProvider)
        {
            case "Facebook":
                externalLoginVm.FacebookRegistrationVm = FacebookTemplates.RegistrationVm;
                return await FacebookLoginAsync(externalLoginVm);
        }
        return View("Login");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FacebookLoginAsync(ExternalLoginVm externalLoginVm)
    {
        var request = await _authentication.LoginWithFacebookAsync(externalLoginVm.AccessToken, externalLoginVm.FacebookRegistrationVm);
        //Login
        if (request.IsLoginSuccessful)
        {
            await SetLoginWithHttpCookieAsync(request.UserLoginAttempt.ApplicationUserDto, request.UserLoginAttempt.JWToken);
            if (User.IsInRole(RoleTypes.Guest))
            {
                TempData["Login"] = true;
                TempData["Type"] = SweetAlertHelper.Types.Success;
                if (!string.IsNullOrEmpty(externalLoginVm.ReturnUrl))
                    return Redirect(externalLoginVm.ReturnUrl);
                return RedirectToAction("Dashboard", "User");
            }
            return RedirectToAction(nameof(Logout));
        }

        //Email Confirmation: false
        if (request.UserLoginAttempt != null && request.IsRegistrationSuccessful)
        {
            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Warning;
            TempData["Title"] = SweetAlertHelper.Titles.InvalidOperation;
            TempData["Message"] = SweetAlertHelper.Messages.EmailConfirmationRequired;
            return RedirectToAction("SendEmailConfirmation", "Register");
        }
        //Just Registered
        if (request.IsRegistrationSuccessful)
        {
            var token = await _authentication.GetEmailConfirmationToken(request.UserRegistrationAttempt.ApplicationUserDto.Email);
            if (!token.IsSuccessful)
            {
                return RedirectToAction("SendEmailConfirmation", "Register");
            }

            var emailConfirmationToken = (string)token.ResponseObject!;
            var callbackUrl = Url.Action(
                controller: "Register",
                action: "EmailConfirmation",
                values: new { email = request.UserRegistrationAttempt.ApplicationUserDto.Email, emailConfirmationToken},
                protocol: Request.Scheme
            );

            var emailMessage = await _mailJet.CreateHtmlMessageCallbackAnchorValueTemplate(
                EmailTemplate.Message.EmailConfirmation,
                callbackUrl,
                EmailTemplate.Anchor.EmailConfirmation);
            await _mailJet.MailJetMailSender(request.UserRegistrationAttempt.ApplicationUserDto.Email, EmailTemplate.Title.EmailConfirmation, emailMessage);

            return RedirectToAction("SendEmailConfirmation", "Register");
        }

        //HTTP-469(Unassigned) : Api Requests More Information to Comply Successfully
        if (request.StatusCode == 469)
        {
            var facebookRegistrationVm = new FacebookRegistrationVm
            {
                FirstName = request.UserRegistrationAttempt.ApplicationUserDto.FirstName,
                LastName = request.UserRegistrationAttempt.ApplicationUserDto.LastName
            };

            var newExternalLoginVm = new ExternalLoginVm
            {
                LoginProvider = externalLoginVm.LoginProvider,
                AccessToken = externalLoginVm.AccessToken,
                FacebookRegistrationVm = facebookRegistrationVm
            };

            return View("ExternalLogin", newExternalLoginVm);
        }

        //request.StatusCode = 407(Proxy Authentication Required) : Invalid OAuth Access Token
        return RedirectToAction("Register", "Register");
    }

    #endregion

    #region Forgot Password

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        if (TempData["Swal"] is not null && (bool)TempData["Swal"])
        {
            TempData["Type"] = TempData["Type"];
            TempData["Title"] = TempData["Title"];
            TempData["Message"] = TempData["Message"];
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPasswordAsync([EmailAddress] string userEmail)
    {
        if (!ModelState.IsValid)
            return View();
        #region Tutorial Crud Blocker
        if (await _tutorial.BlockUserCrudAsync(userEmail))
        {
            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Info;
            TempData["Title"] = SweetAlertHelper.Titles.TutorialCrudBlock;
            TempData["Message"] = SweetAlertHelper.Messages.TutorialCrudBlock;
            return RedirectToAction(nameof(Login));
        }
        #endregion

        var request = await _authentication.GetResetPasswordTokenAsync(userEmail);
        if (request.IsSuccessful)
        {
            var callbackUrl = Url.Action(
            controller: "Login",
            action: "ForgotPasswordCallback",
            values: new { email = userEmail, resetPasswordToken = request.ResponseObject.ToString() },
            protocol: Request.Scheme);

            var emailMessage = await _mailJet.CreateHtmlMessageCallbackAnchorValueTemplate(
                EmailTemplate.Message.ForgotPassword,
                callbackUrl,
                EmailTemplate.Anchor.ForgotPassword);

            await _mailJet.MailJetMailSender(userEmail, EmailTemplate.Title.ForgotPassword, emailMessage);

            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Success;
            TempData["Title"] = SweetAlertHelper.Titles.EmailSent;
            TempData["Message"] = string.Format(SweetAlertHelper.Messages.EmailSent, userEmail);
            return View();
        }
        TempData["Swal"] = true;
        TempData["Type"] = SweetAlertHelper.Types.Error;
        TempData["Title"] = SweetAlertHelper.Titles.InvalidOperation;
        TempData["Message"] = SweetAlertHelper.Messages.InvalidResetPasswordToken;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ForgotPasswordCallback(string email, string resetPasswordToken)
    {
        var resetPassword = new ResetPasswordVm();
        resetPassword.UserEmail = email;
        resetPassword.ResetPasswordToken = resetPasswordToken;

        var request = await _authentication.ValidateResetPasswordTokenAsync(email, resetPasswordToken);
        if (request.IsSuccessful)
        {
            return View("ResetPassword", resetPassword);
        }
        TempData["Swal"] = true;
        TempData["Type"] = SweetAlertHelper.Types.Error;
        TempData["Title"] = SweetAlertHelper.Titles.InvalidOperation;
        TempData["Message"] = SweetAlertHelper.Messages.InvalidResetPasswordToken;
        return RedirectToAction(nameof(ForgotPassword));
    }

    [HttpPost]
    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordVm resetPasswordVm)
    {
        if (!ModelState.IsValid)
            return View(resetPasswordVm);
        #region Tutorial Crud Blocker
        if (await _tutorial.BlockUserCrudAsync(resetPasswordVm.UserEmail))
        {
            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Info;
            TempData["Title"] = SweetAlertHelper.Titles.TutorialCrudBlock;
            TempData["Message"] = SweetAlertHelper.Messages.TutorialCrudBlock;
            return RedirectToAction(nameof(Login));
        }
        #endregion
        var request = await _authentication.ResetPasswordAsync(resetPasswordVm);
        if (request.IsSuccessful)
        {
            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Success;
            TempData["Title"] = SweetAlertHelper.Titles.UpdateSuccess;
            TempData["Message"] = SweetAlertHelper.Messages.PasswordUpdate;
            return RedirectToAction(nameof(Login));
        }
        TempData["Swal"] = true;
        TempData["Type"] = SweetAlertHelper.Types.Error;
        TempData["Title"] = SweetAlertHelper.Titles.InvalidOperation;
        TempData["Message"] = SweetAlertHelper.Messages.InvalidResetPasswordToken;
        return RedirectToAction(nameof(ForgotPassword));
    }

    #endregion

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear();
        var cookieKeys = HttpContext.Request.Cookies.Keys;
        foreach (var cookie in cookieKeys)
            HttpContext.Response.Cookies.Delete(cookie);
        var sessionKeys = HttpContext.Session.Keys;
        foreach (var key in sessionKeys)
            HttpContext.Session.Remove(key);
        HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()));
        return RedirectToAction("Index", "Home");
    }

    //HelperMethods
    private async Task SetLoginWithHttpCookieAsync(ApplicationUser user, string jwtToken)
    {
        HttpContext.Response.Cookies.Append(JwtBearerDefaults.AuthenticationScheme, jwtToken);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new(ClaimTypes.NameIdentifier, user.Id));
        identity.AddClaim(new(ClaimTypes.Email, user.Email));
        identity.AddClaim(new(ClaimTypes.GivenName, user.FirstName));
        identity.AddClaim(new(ClaimTypes.Surname, user.LastName));
        identity.AddClaim(new(ClaimTypes.MobilePhone, user.PhoneNumber));
        identity.AddClaim(new(ClaimTypes.StreetAddress, user.Address));
        identity.AddClaim(new(ClaimTypes.Locality, user.City));
        identity.AddClaim(new(ClaimTypes.StateOrProvince, user.StateOrProvince));
        identity.AddClaim(new(ClaimTypes.Authentication, user.HasPassword.ToString()));
        identity.AddClaims(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var principal = new ClaimsPrincipal(identity);
        HttpContext.User = principal;
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }

}

