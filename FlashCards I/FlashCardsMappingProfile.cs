using AutoMapper;
using FlashCards.Entities;
using FlashCards_I.Models;

namespace FlashCards_I
{
    public class FlashCardsMappingProfile : Profile
    {
        public FlashCardsMappingProfile()
        {
            CreateMap<Stack,StackDto>();
            CreateMap<WordAndDef, WordAndDefDto>();
            CreateMap<CreateStackDto, Stack>();
            CreateMap<UpdateStackDto, Stack>();
        }
    }
}
