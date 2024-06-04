using AutoMapper;
using LantanaComfyAPI.Dto;
using LantanaComfyAPI.Models;

namespace LantanaComfyAPI.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Reservation, ReservationDto>();
        CreateMap<Contact, ContactDto>();
        CreateMap<Table, TableDto>();
        CreateMap<ReservationDto, Reservation>();
        CreateMap<ContactDto, Contact>();
        CreateMap<TableDto, Table>();
    } 
}