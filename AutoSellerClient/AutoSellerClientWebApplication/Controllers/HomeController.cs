using Microsoft.AspNetCore.Mvc;
using Models.ListedVehicleModels;
using Models.PagesViewModels;
using Models.ResponseModels;
using Services.ApiRouteServices;
using Services.IndexPageVnService;
using Services.RepositoryServices.ListedVehiclesRepository;
using Services.RepositoryServices.MakersRepository;


namespace AutoSellerClientWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiRoute _route;
        private readonly IListedVehicleRepository _listedVehicleRepository;
        private readonly IIndexPageVmService _indexPageVmService;
        private readonly IMakersRepository _makers;
        public HomeController(IListedVehicleRepository listedVehicleRepository, IIndexPageVmService indexPageVmService, IMakersRepository makers, IApiRoute route)
        {
            _listedVehicleRepository = listedVehicleRepository;
            _indexPageVmService = indexPageVmService;
            _makers = makers;
            _route = route;
        }

        public async Task<IActionResult> Index(int? currentPage, int? pageSize)
        {
            currentPage ??= 1;
            pageSize ??= 6;
            var request = await _listedVehicleRepository.GetAllByPages(_route.GetAllListedVehiclesWithPages(), (int)pageSize, (int)currentPage);
            if (!request.IsSuccessful)
                return View(new IndexPageVm());

            var indexPageVm = await ResponseCreator(request, (int)pageSize, (int)currentPage);
            return View(indexPageVm);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAsync(SearchVm searchByVm)
        {
            var request = await _listedVehicleRepository.GetAllBySearchAsync(searchByVm);
            var indexPageVm = await ResponseCreator(request, searchByVm.pageSize, searchByVm.CurrentPage);
            return PartialView("_VehiclesSearchResult", indexPageVm);
        }

        //Helper Methods
        [HttpGet]
        private async Task<IndexPageVm> ResponseCreator(Response request, int pageSize, int currentPage)
        {
            var makersObjects = await _makers.GetAll(_route.GetAllMakers());
            var makersMapped = await _makers.MapeManyMakersAsync(makersObjects.ResponseObject);
            if (!request.IsSuccessful)
                return new IndexPageVm()
                {
                    CurrentPage = 0,
                    PageSize = 0,
                    ListedVehiclesList = new List<ListedVehicle>(),
                    ListOfMakers = makersMapped,
                    TotalPages = 0
                };

            var listedVehicles = await _listedVehicleRepository.MapManyListedVehiclesAsync(request.ResponseObject);
            var indexPageVm = await _indexPageVmService.GetIndexPageVmAsync(listedVehicles, makersMapped, request.TotalResponseCount, pageSize, currentPage);
            return indexPageVm;
        }

        [HttpGet]
        public async Task<IActionResult> VehicleDetails(string listedVehicleId)
        {
            var request = await _listedVehicleRepository.Get(_route.GetAListedVehiclesByListedVehicleId() + listedVehicleId);
            if (request.IsSuccessful)
            {
                var listedVehicle = await _listedVehicleRepository.MapSingleListedVehicleAsync(request.ResponseObject);
                return View(listedVehicle);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}