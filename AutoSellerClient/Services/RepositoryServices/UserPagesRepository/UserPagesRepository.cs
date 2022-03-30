using System.Net.Http.Headers;
using System.Text;
using AutoMapper;
using Models.ApplicationUserModels;
using Models.ImagesModels;
using Models.IntentModels;
using Models.ListedVehicleModels;
using Models.PagesViewModels.UserPagesViewModels;
using Models.ResponseModels;
using Newtonsoft.Json;
using Services.ApiRouteServices;
using Services.JwtBearerTokens;

namespace Services.RepositoryServices.UserPagesRepository;

public class UserPagesRepository : IUserPagesRepository
{
    private readonly IApiRoute _route;
    private readonly IHttpClientFactory _client;
    private readonly IMapper _mapper;

    public UserPagesRepository(IHttpClientFactory client, IMapper mapper, IApiRoute route)
    {
        _client = client;
        _mapper = mapper;
        _route = route;
    }
    public async Task<DashboardVm> GetDashboardListedVehiclesAsync(string userId, string jwtToken)
    {
        DashboardVm dashboard = new();
        var jwtTokenHeader = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);
        //Listed Vehicles
        {
            var listedVehiclesRequest =
                new HttpRequestMessage(HttpMethod.Get, _route.UserGetAllListedVehicles() + userId);
            listedVehiclesRequest.Headers.Authorization = jwtTokenHeader;
            var listedVehiclesClient = await _client.CreateClient().SendAsync(listedVehiclesRequest);

            dashboard.ListedVehicles = new List<ListedVehicle>();
            if (listedVehiclesClient.IsSuccessStatusCode)
            {
                var listedVehiclesString = await listedVehiclesClient.Content.ReadAsStringAsync();
                var listedVehiclesResponse = JsonConvert.DeserializeObject<Response>(listedVehiclesString);
                var listedVehicles = JsonConvert.DeserializeObject<List<ListedVehicle>>(listedVehiclesResponse.ResponseObject.ToString());
                dashboard.ListedVehicles = listedVehicles!;
            }
        }
        //Sold Listed Vehicles
        {
            var soldListedVehiclesRequest =
                new HttpRequestMessage(HttpMethod.Get, _route.UserGetAllSoldListedVehicles() + userId);
            soldListedVehiclesRequest.Headers.Authorization = jwtTokenHeader;
            var soldListedVehiclesClient = await _client.CreateClient().SendAsync(soldListedVehiclesRequest);

            dashboard.SoldListedVehicles = new List<ListedVehicleSoldVm>();
            if (soldListedVehiclesClient.IsSuccessStatusCode)
            {
                var soldListedVehiclesString = await soldListedVehiclesClient.Content.ReadAsStringAsync();
                var soldListedVehiclesResponse = JsonConvert.DeserializeObject<Response>(soldListedVehiclesString);
                var soldListedVehicles = JsonConvert.DeserializeObject<List<ListedVehicleSoldVm>>(soldListedVehiclesResponse.ResponseObject.ToString());
                dashboard.SoldListedVehicles = soldListedVehicles!;
            }
        }
        return dashboard;
    }
    public async Task<List<Intents>> GetReceivedIntentsAsync(string userId, string jwtToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, _route.UserGetIntentsReceived() + userId);
        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        if (response.IsSuccessful)
        {
            var receivedIntents = _mapper.Map<List<Intents>>(response.ResponseObject);
            return receivedIntents;
        }
        return new List<Intents>();
    }
    public async Task<List<Intents>> GetSentIntentsAsync(string userId, string jwtToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, _route.UserGetIntentsSent() + userId);
        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        if (response.IsSuccessful)
        {
            var sentIntents = _mapper.Map<List<Intents>>(response.ResponseObject);
            return sentIntents;
        }
        return new List<Intents>();
    }
    public async Task<Response> RegisterVehicleAsync(ListedVehicleCreateVm listedVehicleCreateVm, string jwtToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _route.UserRegisterVehicle());
        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);
        if (string.IsNullOrWhiteSpace(listedVehicleCreateVm.Description))
            listedVehicleCreateVm.Description = "None";
        request.Content = new StringContent(JsonConvert.SerializeObject(listedVehicleCreateVm), Encoding.UTF8,
            "application/json");

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        return response!;
    }
    public async Task<IEnumerable<Response>> AddImagesToListedVehicleAsync(string jwtToken, IEnumerable<ImagesCreateVm> imagesCreateVm, string listedVehicleId)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _route.UserAddImagesToListedVehicle() + listedVehicleId);
        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);
        request.Content =
            new StringContent(JsonConvert.SerializeObject(imagesCreateVm), Encoding.UTF8, "application/json");

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var responses = JsonConvert.DeserializeObject<IEnumerable<Response>>(responseString);
        return responses!;
    }
    public async Task<bool> DeleteImageAsync(string imageId, string jwtToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, _route.UserDeleteImage() + imageId);
        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);
        var client = await _client.CreateClient().SendAsync(request);

        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        return response.IsSuccessful;
    }
    public async Task<Response> MakeVehicleBuyIntentAsync(IntentsCreateVm intentCreateVm, string jwtToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _route.UserMakeVehicleBuyIntent());
        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);
        request.Content = new StringContent(JsonConvert.SerializeObject(intentCreateVm), Encoding.UTF8, "application/json");

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        return response;
    }
    public async Task<Response> SetVehicleAsSoldAsync(string intentId, string applicationUserId, string jwtToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_route.UserSetListedVehicleAsSold()}{intentId}/{applicationUserId}");
        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        return response;
    }
    public async Task<Response> ToggleIntentIsReadAsync(string intentId, string applicationUserId, string jwtToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"{_route.UserToggleIntentIsRead()}{intentId}/{applicationUserId}");
        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        return response;
    } 
    public async Task<Response> SetIntentAsDiscardedAsync(string intentId, string applicationUserId, string jwtToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"{_route.UserSetIntentAsDiscarded()}{intentId}/{applicationUserId}");
        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        return response;
    }


    public async Task<List<Response>> MoveImagesAsync(IList<ImageUpdateVm> imageUpdate, string jwtToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, _route.UserUpdateImage());
        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);
        request.Content = new StringContent(JsonConvert.SerializeObject(imageUpdate), Encoding.UTF8, "application/Json");

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        var responseList = JsonConvert.DeserializeObject<List<Response>>(response.ResponseObject.ToString());
        return responseList!;
    }
    public Task<ListedVehicle> MapListedVehicleAsync(object listedVehicleObject)
    {
        var listedVehicle = JsonConvert.DeserializeObject<ListedVehicle>(listedVehicleObject.ToString());
        return Task.FromResult(listedVehicle);
    }
    public Task<ApplicationUser> MapApplicationUserLoginResponseAsync(object loginResponseObject)
    {
        var user = _mapper.Map<ApplicationUser>(loginResponseObject);
        return Task.FromResult(user);
    }
    public Task<ListedVehicleUpdateVm> MapListedVehicleUpdateAsync(ListedVehicle listedVehicle)
    {
        return Task.FromResult(_mapper.Map<ListedVehicleUpdateVm>(listedVehicle));
    }
    public Task<ListedVehicleCreateVm> MapListedVehicleCreateVmAsync(RegisterAVehicleVm listedVehicleObject)
    {
        return Task.FromResult(_mapper.Map<ListedVehicleCreateVm>(listedVehicleObject));
    }
    public Task<Images> MapImagesAsync(object imageObject)
    {
        return Task.FromResult(JsonConvert.DeserializeObject<Images>(imageObject.ToString()));
    }

    public Task<Intents> MapIntentAsync(object intentObject)
    {
        var intent = _mapper.Map<Intents>(intentObject);
        return Task.FromResult(intent);
    }
}