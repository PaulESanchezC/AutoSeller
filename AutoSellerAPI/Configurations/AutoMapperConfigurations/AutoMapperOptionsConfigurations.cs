using AutoMapper;
using Models.ApplicationUsersModels;
using Models.AuthenticationModels;
using Models.ImagesModels;
using Models.IntentsModels;
using Models.ListedVehiclesModels;
using Models.MakerModels;
using Models.VehiclesModels;

namespace Configurations.AutoMapperConfigurations;

public class AutoMapperOptionsConfigurations : Profile
{
    public AutoMapperOptionsConfigurations()
    {
        //Vehicle Maps
        CreateMap<Vehicle, VehicleDto>().ReverseMap();
        CreateMap<VehicleCreateDto, Vehicle>().ReverseMap();
        CreateMap<VehicleUpdatetDto, Vehicle>().ReverseMap();
        CreateMap<VehicleUpdatetDto, VehicleDto>().ReverseMap();
        CreateMap<VehicleCreateDto, VehicleDto>().ReverseMap();

        //Maker Maps
        CreateMap<Maker, MakerDto>().ReverseMap();
        CreateMap<MakerCreateDto, Maker>().ReverseMap();
        CreateMap<MakerUpdateDto, Maker>().ReverseMap();
        CreateMap<MakerUpdateDto, MakerDto>().ReverseMap();
        CreateMap<MakerCreateDto, MakerDto>().ReverseMap();

        //ApplicationUser Maps
        CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
        CreateMap<ApplicationUser, UserRegistrationDto>().ReverseMap();

        //Intent Maps
        CreateMap<Intent, IntentDto>().ReverseMap();
        CreateMap<IntentCreateDto, Intent>().ReverseMap();
        CreateMap<IntentCreateDto, IntentDto>().ReverseMap();
        CreateMap<IntentUpdateDto, Intent>().ReverseMap();
        CreateMap<IntentUpdateDto, IntentDto>().ReverseMap();

        //Listed Vehicles Maps
        CreateMap<ListedVehicle, ListedVehicleDto>().ReverseMap();
        CreateMap<ListedVehicleCreateDto, ListedVehicle>().ReverseMap();
        CreateMap<ListedVehicleUpdateDto, ListedVehicle>().ReverseMap();
        CreateMap<ListedVehicleUpdateDto, ListedVehicleDto>().ReverseMap();
        CreateMap<ListedVehicleCreateDto, ListedVehicleDto>().ReverseMap();

        //Image Maps
        CreateMap<Image, ImageDto>().ReverseMap();
        CreateMap<Image, ImageCreateDto>().ReverseMap();
        CreateMap<Image, ImageUpdateDto>().ReverseMap();
        CreateMap<ImageDto, ImageCreateDto>().ReverseMap();
        CreateMap<ImageDto, ImageUpdateDto>().ReverseMap();
    }
}