using AutoMapper;
using ToDoApi.DTOs;
using ToDoCore.Models;

namespace ToDoApi.MappingConfigurations
{
    public class ToDoListProfile : Profile
    {
        public ToDoListProfile()
        {
            CreateMap<ToDoList, ToDoListDto>();
            CreateMap<ToDoListDto, ToDoList>().ForMember(l => l.Id, opt => opt.Ignore());
        }
    }
}
