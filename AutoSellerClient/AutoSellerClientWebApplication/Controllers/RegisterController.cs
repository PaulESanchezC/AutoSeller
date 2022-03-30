using Microsoft.AspNetCore.Mvc;
using Models.ApplicationUserModels;
using Models.PagesViewModels;
using Services.AuthenticationService;
using Services.EmailServices;
using Services.EmailServices.EmailTemplates;
using Services.RepositoryServices.AplicationUsesrRepository;
using Services.StaticData.SweetAlertTemplates;

namespace AutoSellerClientWebApplication.Controllers;

public class RegisterController : Controller
{
    private readonly IAuthenticationServices _authentication;
    private readonly IApplicationUserRepository _applicationUser;
    private readonly IMailJetEmailSender _mailJet;

    public RegisterController(IAuthenticationServices authentication, IApplicationUserRepository applicationUser, IMailJetEmailSender mailJet)
    {
        _authentication = authentication;
        _applicationUser = applicationUser;
        _mailJet = mailJet;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UserRegistrationForm(ApplicationUserRegisterVm applicationUserRegisterVm)
    {
        if (applicationUserRegisterVm.ExternalLogin != "local"
            && applicationUserRegisterVm.AccessToken != "none")
        {
            var externalLoginVm = new ExternalLoginVm
            {
                AccessToken = applicationUserRegisterVm.AccessToken,
                LoginProvider = applicationUserRegisterVm.ExternalLogin,
            };
            return RedirectToAction("ExternalLogin", "Login", externalLoginVm);
        }

        if (!ModelState.IsValid)
            return View(nameof(Register), applicationUserRegisterVm);

        var request = await _authentication.RegisterUserAsync(applicationUserRegisterVm);
        if (!request.IsSuccessful)
            return View(nameof(Register), applicationUserRegisterVm);

        var user = await _applicationUser.MapApplicationUserFromObject(request.ResponseObject);

        var callbackUrl = Url.Action(
            controller: nameof(Register),
            action: nameof(EmailConfirmation),
            values: new { email = user.Email, emailConfirmationToken = user.Token },
            protocol: Request.Scheme
            );

        var emailMessage = await _mailJet.CreateHtmlMessageCallbackAnchorValueTemplate(
            EmailTemplate.Message.EmailConfirmation,
            callbackUrl,
            EmailTemplate.Anchor.EmailConfirmation);
        await _mailJet.MailJetMailSender(user.Email, EmailTemplate.Title.EmailConfirmation, emailMessage);

        TempData["Swal"] = true;
        TempData["Type"] = SweetAlertHelper.Types.Success;
        TempData["Title"] = SweetAlertHelper.Titles.EmailSent;
        TempData["Message"] = string.Format(SweetAlertHelper.Messages.ProfileRegister, user.Email);
        return View(nameof(SendEmailConfirmation), user.Email);
    }

    [HttpGet]
    public async Task<IActionResult> EmailConfirmation(string email, string emailConfirmationToken)
    {
        if (string.IsNullOrEmpty(emailConfirmationToken))
            return RedirectToAction("SendEmailConfirmation");

        var request = await _authentication.ValidateEmailConfirmationToken(email, emailConfirmationToken);
        if (request.IsSuccessful)
            return View(new EmailConfirmationPageVm
            {
                Email = email,
                IsUserConfirmed= true
            });
        TempData["Swal"] = true;
        TempData["Type"] = SweetAlertHelper.Types.Error;
        TempData["Title"] = SweetAlertHelper.Titles.InvalidOperation;
        TempData["Message"] = SweetAlertHelper.Messages.InvalidEmailConfirmation;
        return View(nameof(SendEmailConfirmation), email);
    }

    [HttpGet]
    public IActionResult SendEmailConfirmation(string? email = null)
    {
        if (TempData["Swal"] is not null && (bool)TempData["Swal"])
        {
            TempData["Type"] = TempData["Type"];
            TempData["Title"] = TempData["Title"];
            TempData["Message"] = TempData["Message"];
        }
        return View(email);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SendEmailConfirmationAsync(string email)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var token = await _authentication.GetEmailConfirmationToken(email);
        if (!token.IsSuccessful)
        {
            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Error;
            TempData["Title"] = token.Title;
            TempData["Message"] = token.Message;
            return View();
        }

        var emailConfirmationToken = (string)token.ResponseObject!;
        var callbackUrl = Url.Action(
            controller: nameof(Register),
            action: nameof(EmailConfirmation),
            values: new { email = email, emailConfirmationToken },
            protocol: Request.Scheme
        );
        var emailMessage = await _mailJet.CreateHtmlMessageCallbackAnchorValueTemplate(
            EmailTemplate.Message.EmailConfirmation,
            callbackUrl,
            EmailTemplate.Anchor.EmailConfirmation);
        await _mailJet.MailJetMailSender(email, EmailTemplate.Title.EmailConfirmation, emailMessage);

        TempData["Swal"] = true;
        TempData["Type"] = SweetAlertHelper.Types.Success;
        TempData["Title"] = SweetAlertHelper.Titles.EmailSent;
        TempData["Message"] = string.Format(SweetAlertHelper.Messages.EmailSent, email);
        return View();
    }
}