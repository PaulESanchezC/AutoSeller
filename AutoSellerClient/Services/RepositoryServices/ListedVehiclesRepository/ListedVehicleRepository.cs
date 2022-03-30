using AutoMapper;
using Microsoft.AspNetCore.Http;
using Models.ListedVehicleModels;
using Models.PagesViewModels;
using Models.ResponseModels;
using Newtonsoft.Json;
using Services.ApiRouteServices;
using Services.RepositoryServices.CrudRepository;

namespace Services.RepositoryServices.ListedVehiclesRepository;

public class ListedVehicleRepository : CrudRepository<ListedVehicle, ListedVehicleCreateVm, ListedVehicleUpdateVm>, IListedVehicleRepository
{
    private readonly IApiRoute _route;
    private readonly IHttpClientFactory _httpClientFactory;
    public ListedVehicleRepository(HttpClient httpClient, IMapper mapper, IHttpClientFactory httpClientFactory, IApiRoute route) : base(httpClient, mapper)
    {
        _httpClientFactory = httpClientFactory;
        _route = route;
    }

    public Task<IList<ListedVehicle>> MapManyListedVehiclesAsync(object listedVehicleList)
    {
        return Task.FromResult(JsonConvert.DeserializeObject<IList<ListedVehicle>>(listedVehicleList.ToString()));
    }
    public Task<ListedVehicle> MapSingleListedVehicleAsync(object listedVehicle)
    {
        var mappedVehicle = JsonConvert.DeserializeObject<ListedVehicle>(listedVehicle.ToString()!);
        return Task.FromResult(mappedVehicle!);
    }
    public async Task<Response> GetAllBySearchAsync(SearchVm searchByVm)
    {
        var route = new QueryString();
        var type = searchByVm.GetType();

        foreach (var propertyInfo in type.GetProperties())
            route += QueryString.Create(propertyInfo.Name, propertyInfo.GetValue(searchByVm)?.ToString()!);
        
        
        var request = new HttpRequestMessage(HttpMethod.Get, _route.GetAllListedVehiclesBySearch()+route);
        var client = await _httpClientFactory.CreateClient().SendAsync(request);

        var jsonStringResponse = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(jsonStringResponse);
        return response;
    }

}