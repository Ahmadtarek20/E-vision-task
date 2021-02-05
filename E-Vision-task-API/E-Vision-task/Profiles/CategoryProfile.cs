using AutoMapper;
using E_Vision_task.Domain;
using E_Vision_task.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Vision_task.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ForMember(d => d.Photo, opts => opts.MapFrom(src => src.PhotoPath))
                .ReverseMap();
            CreateMap<ProductCreation, Product>().ReverseMap();
            CreateMap<ProductEditeDto, Product>().ReverseMap();
        }
    }
}
