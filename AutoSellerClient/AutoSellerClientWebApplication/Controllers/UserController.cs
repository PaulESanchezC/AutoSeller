using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ApplicationUserModels;
using Models.AuthenticationModels;
using Models.ImagesModels;
using Models.IntentModels;
using Models.ListedVehicleModels;
using Models.PagesViewModels.UserPagesViewModels;
using Models.ResponseModels;
using Newtonsoft.Json;
using Services.ApiRouteServices;
using Services.AuthenticationService;
using Services.EmailServices;
using Services.EmailServices.EmailTemplates;
using Services.JwtBearerTokens;
using Services.RepositoryServices.ImageRepository;
using Services.RepositoryServices.IntentsRepository;
using Services.RepositoryServices.ListedVehiclesRepository;
using Services.RepositoryServices.UserPagesRepository;
using Services.RepositoryServices.VehiclesRepository;
using Services.StaticData.SweetAlertTemplates;
using Services.TutorialServices;

namespace AutoSellerClientWebApplication.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly ITutorialService _tutorial;

    private readonly IApiRoute _route;
    private readonly IUserPagesRepository _user;
    private readonly IVehiclesRepository _vehicles;
    private readonly IListedVehicleRepository _listedVehicles;
    private readonly IImageRepository _images;
    private readonly IIntentRepository _intent;
    private readonly IAuthenticationServices _auth;
    private readonly IMailJetEmailSender _mailJet;
    private string UserId { get; set; }
    private string JwtToken { get; set; }

    private string UserFirstName { get; set; }
    private string UserLastName { get; set; }
    private string UserPhone { get; set; }
    private string UserEmail { get; set; }
    public UserController(IUserPagesRepository user, IVehiclesRepository vehicles, IListedVehicleRepository listedVehicles, IImageRepository images, IAuthenticationServices auth, IMailJetEmailSender mailJet, IIntentRepository intent, IApiRoute route, ITutorialService tutorial)
    {
        _user = user;
        _vehicles = vehicles;
        _listedVehicles = listedVehicles;
        _images = images;
        _auth = auth;
        _mailJet = mailJet;
        _intent = intent;
        _route = route;
        _tutorial = tutorial;
        UserId = "";
        JwtToken = "";
    }


    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        if (!await SetUserIdAndJwtTokenTask())
            return RedirectToAction("logout", "Login");

        var dashboard = await _user.GetDashboardListedVehiclesAsync(UserId, JwtToken);

        if (TempData["Login"] is not null && (bool)TempData["Login"])
        {
            TempData["Login"] = TempData["Login"];
            TempData["Type"] = TempData["Type"];
            TempData["Message"] = TempData["Message"];
        }
        return View(dashboard);
    }

    #region Profile

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        if (!await SetUserIdAndJwtTokenTask())
            return RedirectToAction("logout", "Login");

        var profileVm = new ProfileVm();
        var claimsUser = HttpContext.User.Claims;

        profileVm.ApplicationUser.Id = claimsUser.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(v => v.Value)
            .SingleOrDefault();
        profileVm.ApplicationUser.Email = claimsUser.Where(c => c.Type == ClaimTypes.Email).Select(v => v.Value)
            .SingleOrDefault();
        profileVm.ApplicationUser.FirstName = claimsUser.Where(c => c.Type == ClaimTypes.GivenName).Select(v => v.Value)
            .SingleOrDefault();
        profileVm.ApplicationUser.LastName = claimsUser.Where(c => c.Type == ClaimTypes.Surname).Select(v => v.Value)
            .SingleOrDefault();
        profileVm.ApplicationUser.PhoneNumber = claimsUser.Where(c => c.Type == ClaimTypes.MobilePhone).Select(v => v.Value)
            .SingleOrDefault();
        profileVm.ApplicationUser.Address = claimsUser.Where(c => c.Type == ClaimTypes.StreetAddress).Select(v => v.Value)
            .SingleOrDefault();
        profileVm.ApplicationUser.City = claimsUser.Where(c => c.Type == ClaimTypes.Locality).Select(v => v.Value)
            .SingleOrDefault();
        profileVm.ApplicationUser.StateOrProvince = claimsUser.Where(c => c.Type == ClaimTypes.StateOrProvince).Select(v => v.Value)
            .SingleOrDefault();
        profileVm.ApplicationUser.HasPassword = Boolean.Parse(claimsUser.Where(c => c.Type == ClaimTypes.Authentication).Select(v => v.Value)
            .SingleOrDefault());

        profileVm.SetLoginPasswordVm.ApplicationUserEmail = profileVm.ApplicationUser.Email;
        profileVm.ResetPasswordVm.UserEmail = profileVm.ApplicationUser.Email;
        profileVm.SetLoginPasswordVm.CurrentPassword = "";
        profileVm.SetLoginPasswordVm.NewPassword = "";

        if (TempData["Swal"] is not null && (bool)TempData["Swal"])
        {
            TempData["Type"] = TempData["Type"];
            TempData["Title"] = TempData["Title"];
            TempData["Message"] = TempData["Message"];
        }
        return View(profileVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfile(ApplicationUser applicationUserUpdate)
    {
        if (!await SetUserIdAndJwtTokenTask())
            return RedirectToAction("logout", "Login");
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Profile));
        #region Tutorial Crud Blocker
        if (await _tutorial.BlockUserCrudAsync(UserId, UserEmail))
        {
            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Info;
            TempData["Title"] = SweetAlertHelper.Titles.TutorialCrudBlock;
            TempData["Message"] = SweetAlertHelper.Messages.TutorialCrudBlock;
            return RedirectToAction(nameof(Dashboard));
        }
        #endregion
        var request = await _auth.UpdateUserProfileAsync(applicationUserUpdate, JwtToken);
        if (request.LoginSuccessful)
        {
            var user = await _user.MapApplicationUserLoginResponseAsync(request.ApplicationUserDto);
            await SetLoginAsync(user, request.JWToken);

            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Success;
            TempData["Title"] = SweetAlertHelper.Titles.UpdateSuccess;
            TempData["Message"] = SweetAlertHelper.Messages.ProfileUpdate;

            return RedirectToAction(nameof(Profile));
        }
        return RedirectToAction(nameof(Profile));
    }

    [HttpPost]
    public async Task<IActionResult> SetUserLoginPassword(SetLoginPasswordVm setOAuthUserPasswordVm)
    {
        if (!await SetUserIdAndJwtTokenTask())
            return RedirectToAction("logout", "Login");
        if (!ModelState.IsValid)
        {
            if (string.IsNullOrEmpty(setOAuthUserPasswordVm.ApplicationUserEmail))
                return RedirectToAction(nameof(Dashboard));
            return RedirectToAction(nameof(Profile));
        }

        #region Tutorial Crud Blocker
        if (await _tutorial.BlockUserCrudAsync(UserId, UserEmail))
        {
            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Info;
            TempData["Title"] = SweetAlertHelper.Titles.TutorialCrudBlock;
            TempData["Message"] = SweetAlertHelper.Messages.TutorialCrudBlock;
            return RedirectToAction(nameof(Dashboard));
        }
        #endregion
        var request = await _auth.SetLoginPasswordAsync(setOAuthUserPasswordVm, JwtToken);
        if (request.IsSuccessful)
        {
            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Success;
            TempData["Title"] = SweetAlertHelper.Titles.UpdateSuccess;
            TempData["Message"] = SweetAlertHelper.Messages.PasswordUpdate;
            return RedirectToAction(nameof(Profile));
        }
        return RedirectToAction(nameof(Profile));
    }

    #endregion

    #region Vehicles

    [HttpGet]
    public async Task<IActionResult> RegisterAVehicle()
    {
        if (!await SetUserIdAndJwtTokenTask())
            return RedirectToAction("logout", "Login");

        var request = await _vehicles.GetAll(_route.GetAllVehicleModels());
        var registerAVehicleVm = new RegisterAVehicleVm();
        registerAVehicleVm.VehiclesModels = (await _vehicles.MapManyVehiclesAsync(request.ResponseObject)).ToList();
        registerAVehicleVm.MakersList = registerAVehicleVm.VehiclesModels
            .Select(v => v.Maker)
            .DistinctBy(m => m.MakerId).ToList();
        return View(registerAVehicleVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegisterAVehicle(RegisterAVehicleVm registerAVehicleVm)
    {
        if (!await SetUserIdAndJwtTokenTask())
            return RedirectToAction("logout", "Login");
        if (!ModelState.IsValid)
            return View(nameof(RegisterAVehicle), registerAVehicleVm);
        #region Tutorial Crud Blocker
        if (await _tutorial.BlockUserCrudAsync(UserId, UserEmail))
        {
            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Info;
            TempData["Title"] = SweetAlertHelper.Titles.TutorialCrudBlock;
            TempData["Message"] = SweetAlertHelper.Messages.TutorialCrudBlock;
            return RedirectToAction(nameof(Dashboard));
        }
        #endregion
        var listedVehicleCreateVm = await _user.MapListedVehicleCreateVmAsync(registerAVehicleVm);
        var request = await _user.RegisterVehicleAsync(listedVehicleCreateVm, JwtToken);
        if (request.IsSuccessful)
        {
            var listedVehicle = await _user.MapListedVehicleAsync(request.ResponseObject);
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
                await SaveImages(files, listedVehicle);

            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Success;
            TempData["Title"] = SweetAlertHelper.Titles.RegisterSuccess;
            TempData["Message"] = SweetAlertHelper.Messages.VehicleRegister;
            return RedirectToAction(nameof(VehicleDetails), new { listedvehicleId = listedVehicle.ListedVehicleId });
        }
        return View(nameof(RegisterAVehicle), registerAVehicleVm);
    }

    [HttpGet]
    public async Task<IActionResult> VehicleDetails(string? listedVehicleId = null)
    {
        if (!await SetUserIdAndJwtTokenTask())
            return RedirectToAction("logout", "Login");
        if (string.IsNullOrEmpty(listedVehicleId))
            return RedirectToAction(nameof(Dashboard));
        var userVehicleDetailsVm = new UserVehicleDetailsVm();

        var request = await _listedVehicles.Get(_route.GetAListedVehiclesByListedVehicleId() + listedVehicleId);
        if (request.IsSuccessful)
        {
            var dblistedVehicle = await _listedVehicles.MapSingleListedVehicleAsync(request.ResponseObject!);
            userVehicleDetailsVm.ListedVehicle = dblistedVehicle;
            userVehicleDetailsVm.ListedVehicleUpdateVm = await _user.MapListedVehicleUpdateAsync(dblistedVehicle);
            userVehicleDetailsVm.Images = dblistedVehicle.Images;
        }

        TempData["Swal"] ??= false;
        if ((bool)TempData["Swal"])
        {
            TempData["Swal"] = true;
            TempData["Type"] = TempData["Type"].ToString();
            TempData["Title"] = TempData["Title"].ToString();
            TempData["Message"] = TempData["Message"].ToString();
        }
        return View(userVehicleDetailsVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateAVehicle(ListedVehicleUpdateVm userVehicleDetailsVm)
    {
        if (!await SetUserIdAndJwtTokenTask())
            return RedirectToAction("logout", "Login");
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(VehicleDetails), new { listedvehicleId = userVehicleDetailsVm.ListedVehicleId });
        #region Tutorial Crud Blocker
        if (await _tutorial.BlockUserCrudAsync(UserId, UserEmail))
        {
            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Info;
            TempData["Title"] = SweetAlertHelper.Titles.TutorialCrudBlock;
            TempData["Message"] = SweetAlertHelper.Messages.TutorialCrudBlock;
            return RedirectToAction(nameof(VehicleDetails), new { listedvehicleId = userVehicleDetailsVm.ListedVehicleId });
        }
        #endregion
        var request = await _listedVehicles.Update(userVehicleDetailsVm, _route.UserUpdateListedVehicle(), JwtToken);
        if (request.IsSuccessful)
        {
            var files = HttpContext.Request.Form.Files;
            var listedVehicle = await _user.MapListedVehicleAsync(request.ResponseObject);
            if (files.Count > 0)
                await SaveImages(files, listedVehicle);

            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Success;
            TempData["Title"] = SweetAlertHelper.Titles.UpdateSuccess;
            TempData["Message"] = SweetAlertHelper.Messages.VehicleUpdate;
            return RedirectToAction(nameof(VehicleDetails), new { listedvehicleId = listedVehicle.ListedVehicleId });
        }
        return RedirectToAction(nameof(Dashboard));
    }

    [HttpGet]
    public async Task<IActionResult> SoldVehicleDetails(string listedVehicleId)
    {
        if (!await SetUserIdAndJwtTokenTask())
            return RedirectToAction("logout", "Login");

        var route = string.Format(_route.UserGetSoldListedVehicle(), listedVehicleId, UserId);
        var request = await _listedVehicles.Get(route,JwtToken);
        if (request.IsSuccessful)
        {
            var listedVehicle = await _listedVehicles.MapSingleListedVehicleAsync(request.ResponseObject);
            return View(listedVehicle);
        }

        return RedirectToAction(nameof(Dashboard));
    }

    #endregion

    #region Ajax Methods

    [HttpGet]
    public async Task<IActionResult> GetVehicleModelsListByMakerIdAsync(string makerId)
    {
        if (!await SetUserIdAndJwtTokenTask())
            return RedirectToAction("logout", "Login");
        if (string.IsNullOrEmpty(makerId))
            return BadRequest();

        Response request;
        if (makerId == "all")
        {
            request = await _vehicles.GetAll(_route.GetAllVehicleModels());
            return Json(JsonConvert.SerializeObject(request.ResponseObject));
        }

        request = await _vehicles.GetAll(_route.GetAllVehiclesForMakerByMakerId() + makerId, JwtToken);
        var modelsJson = await _vehicles.MapManyVehiclesFromAMakerAsync(request.ResponseObject);
        return Json(modelsJson);
    }

    [HttpGet]
    public async Task<IActionResult> GetReceivedIntentsAsync()
    {
        await SetUserIdAndJwtTokenTask();
        var request = await _user.GetReceivedIntentsAsync(UserId, JwtToken);
        return Json(new { data = request });
    }

    [HttpGet]
    public async Task<IActionResult> GetSentIntentsAsync()
    {
        await SetUserIdAndJwtTokenTask();
        var request = await _user.GetSentIntentsAsync(UserId, JwtToken);
        return Json(new { data = request });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteImageAsync(string imageId)
    {
        if (!await SetUserIdAndJwtTokenTask())
            return RedirectToAction("logout", "Login");
        if (string.IsNullOrEmpty(imageId))
            return BadRequest();
        #region Tutorial Crud Blocker
        if (await _tutorial.BlockUserCrudAsync(UserId, UserEmail))
            return BadRequest();
        #endregion

        var request = await _user.DeleteImageAsync(imageId, JwtToken);
        return Json(request);
    }

    [HttpPut]
    public async Task<IActionResult> ImageIndexMoveAsync(string imageId, string listedVehicleId, int direction)
    {
        if (!await SetUserIdAndJwtTokenTask())
            return RedirectToAction("logout", "Login");
        #region Tutorial Crud Blocker
        if (await _tutorial.BlockUserCrudAsync(UserId, UserEmail))
            return BadRequest();
        #endregion
        var vehicleRequest = await _listedVehicles.Get(_route.GetAListedVehiclesByListedVehicleId() + listedVehicleId);
        var vehicle = new ListedVehicle();
        if (vehicleRequest.IsSuccessful)
            vehicle = await _listedVehicles.MapSingleListedVehicleAsync(vehicleRequest.ResponseObject);

        var imagesUpdateList = await _images.MoveImageAsync(vehicle.Images, imageId, direction);

        var request = await _user.MoveImagesAsync(imagesUpdateList, JwtToken);
        var imagesList = new List<Images>();
        foreach (var response in request)
            if (response.IsSuccessful)
                imagesList.Add(await _user.MapImagesAsync(response.ResponseObject));

        return PartialView("_VehicleImages", imagesList);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteListedVehicleAsync(string listedVehicleId)
    {
        await SetUserIdAndJwtTokenTask();
        if (string.IsNullOrEmpty(listedVehicleId))
            return BadRequest();
        #region Tutorial Crud Blocker
        if (await _tutorial.BlockUserCrudAsync(UserId, UserEmail))
        {
            TempData["Swal"] = true;
            TempData["Type"] = SweetAlertHelper.Types.Info;
            TempData["Title"] = SweetAlertHelper.Titles.TutorialCrudBlock;
            TempData["Message"] = SweetAlertHelper.Messages.TutorialCrudBlock;
            return BadRequest();
        }
        #endregion

        var request = await _listedVehicles.Delete(listedVehicleId, _route.UserDeleteListedVehicle(), JwtToken);
        return Json(request);
    }

    [HttpPost]
    public async Task<IActionResult> SendIntentAsync(string listedVehicleId, string intentReceiverId)
    {
        await SetUserIdAndJwtTokenTask();
        if (string.IsNullOrEmpty(listedVehicleId) || string.IsNullOrEmpty(intentReceiverId))
            return Json(false);
        var request = await _user.MakeVehicleBuyIntentAsync(
            new IntentsCreateVm
            {
                IntentSenderId = UserId,
                IntentReceiverId = intentReceiverId,
                ListedVehicleId = listedVehicleId
            }, JwtToken);

        if (!request.IsSuccessful)
            return Json(request);

        var intentId = (await _user.MapIntentAsync(request.ResponseObject)).IntentId;

        var intentRequest = await _intent.Get(_route.UserGetAIntentByIntentId() + intentId, JwtToken);
        if (!intentRequest.IsSuccessful)
            return Json(new { data = new Response { IsSuccessful = false, Title = "Error", Message = "Failed to send intent", ResponseObject = null, StatusCode = 409, TotalResponseCount = 0 } });

        var intent = await _user.MapIntentAsync(intentRequest.ResponseObject);
        var email = intent.IntentReceiver.Email;

        var callbackUrl = Url.Action(
            controller: "Login",
            action: "login",
            values: null,
            protocol: Request.Scheme);

        var senderContactInfo = await _mailJet.CreateIntentSenderContactInformationHtml(
                UserFirstName, UserLastName, UserPhone, UserEmail);

        var emailMessage = await _mailJet.CreateHtmlMessageCallbackAnchorValueTemplate(
            senderContactInfo, callbackUrl, EmailTemplate.Anchor.IntentReceived);

        await _mailJet.MailJetMailSender(email, EmailTemplate.Title.IntentReceived, emailMessage);
        return Json(new Response
        {
            IsSuccessful = intentRequest.IsSuccessful,
            Title = request.Title,
            Message = request.Message,
            ResponseObject = intent,
            StatusCode = intentRequest.StatusCode,
            TotalResponseCount = 1
        });
    }

    [HttpPost]
    public async Task<IActionResult> SetVehicleAsSoldAsync(string intentId)
    {
        await SetUserIdAndJwtTokenTask();
        #region Tutorial Crud Blocker
        if (await _tutorial.BlockUserCrudAsync(UserId, UserEmail))
            return BadRequest();
        #endregion
        var request = await _user.SetVehicleAsSoldAsync(intentId, UserId, JwtToken);
        if (request.IsSuccessful)
        {
            var listedVehicleSold = await _user.MapListedVehicleAsync(request.ResponseObject);
            return PartialView("_ListedVehicleSoldHtmlCreator", listedVehicleSold);
        }
        return new EmptyResult();
    }

    [HttpPut]
    public async Task<IActionResult> ToggleIntentIsReadAsync(string intentId)
    {
        await SetUserIdAndJwtTokenTask();
        var request = await _user.ToggleIntentIsReadAsync(intentId, UserId, JwtToken);
        return Json(new { data = request.ResponseObject });
    }

    [HttpPut]
    public async Task<IActionResult> SetIntentAsDiscardedAsync(string intentId)
    {
        await SetUserIdAndJwtTokenTask();
        #region Tutorial Crud Blocker
        if (await _tutorial.BlockUserCrudAsync(UserId, UserEmail))
            return BadRequest();

        #endregion
        var request = await _user.SetIntentAsDiscardedAsync(intentId, UserId, JwtToken);
        return Json(new { data = request.IsSuccessful });
    }

    #endregion

    #region Helper Methods

    private async Task SaveImages(IFormFileCollection files, ListedVehicle listedVehicle)
    {
        var imagesToCreate = new List<ImagesCreateVm>();
        var indexCounter = 0;

        foreach (var file in files)
        {
            byte[]? imageBytes = null;
            using (var readStream = file.OpenReadStream())
            {
                using (var memoryStream = new MemoryStream())
                {
                    readStream.CopyTo(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }
            }

            var image = new ImagesCreateVm
            {
                ImageBytes = imageBytes,
                ImageIndex = indexCounter,
                ListedVehicleId = listedVehicle.ListedVehicleId
            };

            imagesToCreate.Add(image);
            image = new ImagesCreateVm();
            indexCounter++;
        }

        var request = await _user.AddImagesToListedVehicleAsync(JwtToken, imagesToCreate, listedVehicle.ListedVehicleId);
        foreach (var response in request)
            if (response.IsSuccessful)
                listedVehicle.Images.Add(await _user.MapImagesAsync(response.ResponseObject!));
    }

    private Task<bool> SetUserIdAndJwtTokenTask()
    {
        if (HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value != null)
            UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(UserId))
            return Task.FromResult(false);

        if (HttpContext.Request.Cookies[JwtBearerDefaults.AuthenticationScheme] != null)
            JwtToken = HttpContext.Request.Cookies[JwtBearerDefaults.AuthenticationScheme];

        if (HttpContext.User.FindFirst(ClaimTypes.GivenName)?.Value != null)
            UserFirstName = HttpContext.User.FindFirst(ClaimTypes.GivenName)?.Value;

        if (HttpContext.User.FindFirst(ClaimTypes.Surname)?.Value != null)
            UserLastName = HttpContext.User.FindFirst(ClaimTypes.Surname)?.Value;

        if (HttpContext.User.FindFirst(ClaimTypes.MobilePhone)?.Value != null)
            UserPhone = HttpContext.User.FindFirst(ClaimTypes.MobilePhone)?.Value;

        if (HttpContext.User.FindFirst(ClaimTypes.Email)?.Value != null)
            UserEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(JwtToken))
            return Task.FromResult(false);
        return Task.FromResult(true);
    }

    //TODO: Refactoring | reused code from LoginController
    public async Task SetLoginAsync(ApplicationUser user, string jwtToken)
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

    #endregion
}


