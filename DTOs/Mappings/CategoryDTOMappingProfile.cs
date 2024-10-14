using AutoMapper;
using CardShop.Model;

namespace CardShop.DTOs.Mappings
{
    public class CategoryDTOMappingProfile : Profile
    {
        public CategoryDTOMappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
