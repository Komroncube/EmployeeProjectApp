using AutoMapper;
using BackEndProject.DTOs;

namespace BackEndProject.Services.Common;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<EmployeeDto, Employee>();
    }
}
