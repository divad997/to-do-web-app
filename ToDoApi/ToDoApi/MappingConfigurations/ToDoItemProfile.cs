using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.DTOs;
using ToDoCore.Models;

namespace ToDoApi.MappingConfigurations
{
    public class ToDoItemProfile : Profile
    {
        public ToDoItemProfile()
        {
            CreateMap<ToDoItem, ToDoItemDto>();
            CreateMap<ToDoItemDto, ToDoItem>().ForMember(i => i.Id, opt => opt.Ignore());
        }
    }
}
