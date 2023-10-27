using AutoMapper;

namespace BackEndProject.Services.Common;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<EmployeeDto, Employee>();
    }
}
