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
            CreateMap<IAdress, AdressDo>();
            CreateMap<IComment, CommentDo>();

            CreateMap<EquipmentDo, Equipment>();
            CreateMap<JamUserDo, JamUser>();
            CreateMap<SongDo, Song>();
            CreateMap<JamEventDo, JamEvent>();
            CreateMap<AdressDo, Adress>();
            CreateMap<CommentDo, Comment>();
        }
    }
}
