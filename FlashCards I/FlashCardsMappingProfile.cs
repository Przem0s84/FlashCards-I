using AutoMapper;
using FlashCards.Entities;
using FlashCards_I.Entities;
using FlashCards_I.Models;

namespace FlashCards_I
{
    public class FlashCardsMappingProfile : Profile
    {
        public FlashCardsMappingProfile()
        {
            CreateMap<FlashCardSet,FlashCardsSetDto>();
            CreateMap<FlashCard, FlashCardDto>();
            CreateMap<CreateFlashCardsSetDto, FlashCardSet>();
            CreateMap<UpdateFlashCardsSetDto, FlashCardSet>();
            CreateMap<CreateFlashCardDto, FlashCard>();
            CreateMap<RegistrationUDto, User>();
        }
    }
}
