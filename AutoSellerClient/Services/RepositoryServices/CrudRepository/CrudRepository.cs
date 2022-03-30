using System.Net.Http.Headers;
using System.Text;
using AutoMapper;
using Models.ResponseModels;
using Newtonsoft.Json;
using Services.JwtBearerTokens;

namespace Services.RepositoryServices.CrudRepository;

public class CrudRepository<T, TCreate, TUpdate> : ICrudRepository<T, TCreate, TUpdate>
    where T : class
    where TCreate : class
    where TUpdate : class
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public CrudRepository(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }
    public async Task<Response?> GetAll(string apiRoute, string? jwtToken = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, apiRoute);

        if (jwtToken != null)
            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);

        var client = await _httpClient.SendAsync(request);

        var jsonStringResponse = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(jsonStringResponse);
        return response;
    }
    public async Task<Response?> GetAllByPages(string apiRoute, int pageSize, int currentPage)
    {
        var route = new StringBuilder();
        route.Append(apiRoute);
        route.Append(pageSize);
        route.Append("/");
        route.Append(currentPage);

        var request = new HttpRequestMessage(HttpMethod.Get, route.ToString());
        var client = await _httpClient.SendAsync(request);

        var jsonStringResponse = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(jsonStringResponse);
        return response;
    }
    public async Task<Response> Get(string apiRoute, string? jwtToken=null)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, apiRoute);
        if (jwtToken is not null)
            request.Headers.Authorization =
                new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);

        var client = await _httpClient.SendAsync(request);

        var jsonStringResponse = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(jsonStringResponse);
        return response;
    }
    public Task<T> Create(TCreate objectToCreate)
    {
        throw new NotImplementedException();
    }
    public async Task<Response> Update(TUpdate objectToUpdate, string apiRoute, string? jwtToken = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, apiRoute);
        if (!string.IsNullOrEmpty(jwtToken))
            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);
        request.Content = new StringContent(JsonConvert.SerializeObject(objectToUpdate), Encoding.UTF8, "application/json");

        var client = await _httpClient.SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        return response;
    }
    public async Task<bool> Delete(string objectTODeleteId, string ApiRoute, string? jwtToken = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, ApiRoute + objectTODeleteId);
        if (!string.IsNullOrEmpty(jwtToken))
            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);

        var client = await _httpClient.SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        return response.IsSuccessful;
    }
}