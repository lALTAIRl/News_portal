using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News_portal.Models;
using News_portal.ViewModels;
using News_portal.DAL.Entities;
using News_portal.BLL.DTO;

namespace News_portal.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewsCreateViewModel, News>();

            CreateMap<News, NewsEditViewModel>();
            CreateMap<NewsEditViewModel, News>();

            CreateMap<News, NewsViewModel>();




            CreateMap<NewsCreateViewModel, NewsDTO>();

            CreateMap<NewsDTO, NewsEditViewModel>();
            CreateMap<NewsEditViewModel, NewsDTO>();

            CreateMap<NewsDTO, NewsViewModel>();
        }
    }
}
