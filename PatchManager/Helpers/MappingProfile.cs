using AutoMapper;
using PatchManager.DTOs;
using PatchManager.Models;

namespace PatchManager.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Patch, PatchDto>().ReverseMap();
            CreateMap<PatchStatus, PatchStatusDto>().ReverseMap();
        }
    }
}
