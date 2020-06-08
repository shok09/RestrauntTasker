using AutoMapper;
using BLL.Mapper;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class OrderTaskDTO : IMapFrom<OrderTask>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal ExecutedPercent { get; set; }
        public int State { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderTask, OrderTaskDTO>()
                .ForMember(d => d.ExecutedPercent, opt => opt.MapFrom(src => src.TaskStatus.ExecutedPercent))
                .ForMember(d => d.State, opt => opt.MapFrom(src => src.TaskStatus.State))
                .ForMember(d => d.BeginDate, opt => opt.MapFrom(src => src.DateInfo.BeginDate))
                .ForMember(d => d.EndDate, opt => opt.MapFrom(src => src.DateInfo.EndDate))
                .ReverseMap()
                .ForPath(s => s.TaskStatus.ExecutedPercent, opt => opt.MapFrom(src => src.ExecutedPercent))
                .ForPath(s => s.TaskStatus.State, opt => opt.MapFrom(src => src.State))
                .ForPath(s => s.DateInfo.BeginDate, opt => opt.MapFrom(src => src.BeginDate))
                .ForPath(s => s.DateInfo.EndDate, opt => opt.MapFrom(src => src.EndDate));
        }
    }
}
