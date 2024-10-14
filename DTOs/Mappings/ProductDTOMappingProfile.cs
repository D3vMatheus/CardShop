using AutoMapper;
using CardShop.Model;

namespace CardShop.DTOs.Mappings
{
    public class ProductDTOMappingProfile : Profile
    {
        public ProductDTOMappingProfile() {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
