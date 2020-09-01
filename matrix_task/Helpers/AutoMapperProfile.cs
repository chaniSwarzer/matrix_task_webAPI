using AutoMapper;
using matrix_task.Entities;
using matrix_task.Model.Hero;
using matrix_task.Model.Trainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace matrix_task.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Trainer, TrainerModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.TrainerModelId, opt => opt.MapFrom(src => src.TrainerId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                ;
            CreateMap<RegisterModel, Trainer>()
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                ;
            CreateMap<Hero, HeroModel>()
                .ForMember(dest => dest.CurrentPower, opt => opt.MapFrom(src => src.CurrentPower))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.HeroId, opt => opt.MapFrom(src => src.HeroId))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.StartingPower, opt => opt.MapFrom(src => src.StartingPower))
                .ForMember(dest => dest.Suit, opt => opt.MapFrom(src => src.Suit))
                .ForMember(dest => dest.AbilityDescription, opt => opt.MapFrom(src => src.Ability.Description))
                ;
            CreateMap<HeroModel, Hero>();
        }
    }
}
