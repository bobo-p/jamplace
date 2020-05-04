using AutoMapper;
using JamPlace.Api.Models;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.Api.Mapper
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<IEquipment, EquipmentViewModel>();
            CreateMap<IJamUser, JamEventUserViewModel>();
            CreateMap<ISong, SongViewModel>().ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.JamEvent.Id));
            CreateMap<IAdress, AddressViewModel>();
            CreateMap<IJamEvent, GetJamEventViewModel>();
            CreateMap<IJamEvent, UserSpecificJamEventViewModel>();

            CreateMap<SongViewModel, ISong>();
        }
    }
}
