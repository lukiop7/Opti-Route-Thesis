using AutoMapper;
using OptiRoute.Application.Features.Products.Commands.CreateProduct;
using OptiRoute.Application.Features.Products.Queries.GetAllProducts;
using OptiRoute.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptiRoute.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}
