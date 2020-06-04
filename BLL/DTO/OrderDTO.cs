using AutoMapper;
using BLL.Mapper;
using DAL.Entities;
using DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class OrderDTO : IMapFrom<Order>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string Manager { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDTO>()
                .ForMember(d => d.Manager, opt => opt.MapFrom(src => src.Chef.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(d => d.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap()
                .ForPath(s => s.Chef.Name, opt => opt.MapFrom(src => src.Manager))
                .ForPath(s => s.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(s => s.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
