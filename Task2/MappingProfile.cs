using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task2.Models;
using Task2.ViewModels;

namespace Task2
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<News, NewsEditViewModel>();
            CreateMap<NewsEditViewModel, News>();

            CreateMap<News, NewsViewModel>();
        }
    }
}
