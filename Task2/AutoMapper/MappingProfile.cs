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

            CreateMap<News, NewsViewModel>();
        }
    }
}
