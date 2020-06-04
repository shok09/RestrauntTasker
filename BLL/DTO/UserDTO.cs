using AutoMapper;
using BLL.Mapper;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class UserDTO : IMapFrom<Staff>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Staff, UserDTO>()
                .ForMember(d => d.Email, opt => opt.MapFrom(src => src.UserContacts.Email))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(src => src.UserContacts.PhoneNumber))
                .ForMember(d => d.Role, opt => opt.MapFrom(src => (int)src.Role))
                .ReverseMap()
                .ForPath(s => s.UserContacts.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(s => s.UserContacts.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForPath(s => s.Role, opt => opt.MapFrom(src => src.Role));
        }
    }
}
