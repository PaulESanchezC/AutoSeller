using AutoMapper;
using Models.ApplicationUserModels;
using Models.ImagesModels;
using Models.ListedVehicleModels;
using Models.MakerModels;
using Models.PagesViewModels.UserPagesViewModels;
using Models.VehicleModels;

namespace Configurations.AutoMapperConfigurations;

public class AddAutoMapperMapConfigurations : Profile
{
    public AddAutoMapperMapConfigurations()
    {

        //Makers Maps
        CreateMap<Makers, MakersCreateVm>().ReverseMap();
        CreateMap<Makers, MakersUpdateVm>().ReverseMap();
        CreateMap<MakersCreateVm, MakersUpdateVm>().ReverseMap();

        //Vehicles Maps
        CreateMap<Vehicles, VehiclesCreateVm>().ReverseMap();
        CreateMap<Vehicles, VehiclesUpdateVm>().ReverseMap();
        CreateMap<VehiclesCreateVm, VehiclesUpdateVm>().ReverseMap();

        //Listed Vehicles
        CreateMap<ListedVehicle, ListedVehicleCreateVm>().ReverseMap();
        CreateMap<ListedVehicle, ListedVehicleUpdateVm>().ReverseMap();
        CreateMap<ListedVehicleCreateVm, ListedVehicleUpdateVm>().ReverseMap();

        //Application User
        CreateMap<ApplicationUser, ApplicationUserRegisterVm>().ReverseMap();
        CreateMap<ApplicationUser, ApplicationUserLoginVm>().ReverseMap();
        CreateMap<ApplicationUserRegisterVm, ApplicationUserLoginVm>().ReverseMap();

        //User: Register a Vehicle
        CreateMap<ListedVehicleCreateVm, RegisterAVehicleVm>().ReverseMap();

        //Images
        CreateMap<Images, ImagesCreateVm>().ReverseMap();
        CreateMap<Images, ImageUpdateVm>().ReverseMap();
    }
}