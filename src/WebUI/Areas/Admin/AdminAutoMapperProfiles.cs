using AutoMapper;
using BaseProjectWebRazor.Areas.Admin.Models;
using Domain.Entities;

namespace BaseProjectWebRazor.Areas.Admin.Profiles
{
    public class AdminAutoMapperProfiles:Profile
    {
        public AdminAutoMapperProfiles()
        {
            CreateMap<LocationDto, Location>().ReverseMap();
            CreateMap<NewCategoryDto, NewCategory>().ReverseMap();
            CreateMap<NewDto, New>().ReverseMap();
            CreateMap<PartnerCategoryDto, PartnerCategory>().ReverseMap();
            CreateMap<ProjectCategoryDto, ProjectCategory>().ReverseMap();
            CreateMap<ProjectDto, Project>().ReverseMap();
            CreateMap<ProjectField, ProjectFieldDto>().ReverseMap();
            CreateMap<AboutProjectField, AboutProjectFieldDto>().ReverseMap();
            CreateMap<AboutProject, AboutProjectDto>().ReverseMap();
            CreateMap<SiteSetting, SiteSettingDto>().ReverseMap();
        }
        
    }
}
