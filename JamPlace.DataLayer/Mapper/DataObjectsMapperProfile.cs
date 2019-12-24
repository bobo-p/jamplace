using AutoMapper;
using JamPlace.DataLayer.Entities;
using JamPlace.DomainLayer.Interfaces.Models;
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
        }
    }
}
