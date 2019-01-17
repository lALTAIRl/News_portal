using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News_portal.Models;
using News_portal.ViewModels;

namespace News_portal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewsCreateViewModel, News>();

            CreateMap<News, NewsEditViewModel>();
            CreateMap<NewsEditViewModel, News>();
                //.ForMember(dest => dest.Caption, opt => opt.Ignore());
                //.ForMember(dest => dest.DateOfCreating, opt => opt.Ignore())
                //.ForMember(dest => dest.IsPublished, opt => opt.Ignore())
                //.ForMember(dest => dest.DateOfPublishing, opt => opt.Ignore());
                //.ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<News, NewsViewModel>();
        }
    }
}
