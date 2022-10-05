using AutoMapper;

namespace Cityinfo.API.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest, Models.PointofInterestDto>();
            CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
        }
    }
}
