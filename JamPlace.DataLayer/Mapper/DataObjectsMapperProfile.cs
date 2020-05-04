using AutoMapper;
using JamPlace.DataLayer.Entities;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer.Mapper
{
    public class DataObjectsMapperProfile : Profile
    {
        public DataObjectsMapperProfile()
        {
            CreateMap<IJamUser, JamUserDo>();
            CreateMap<IEquipment, EquipmentDo>();
            CreateMap<IJamEvent, JamEventDo>();
            CreateMap<ISong, SongDo>();

            CreateMap<EquipmentDo, Equipment>().ForSourceMember(src => src.EquipmentEventEquipmentDos, opt => opt.DoNotValidate());
            CreateMap<JamUserDo, JamUser>();
            CreateMap<SongDo, Song>();
            CreateMap<JamEventDo, JamEvent>();
        }
    }
}
