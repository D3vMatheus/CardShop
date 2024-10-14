using AutoMapper;
using CardShop.Model;

namespace CardShop.DTOs.Mappings
{
    public class CardDTOMappingProfile : Profile
    {
        public CardDTOMappingProfile() {
            CreateMap<Card, CardDTO>().ReverseMap();
        }
    }
}
