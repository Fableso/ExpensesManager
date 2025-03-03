using AutoMapper;

using PrimaryPorts.Models;

using SecondaryPorts.Abstractions;
using SecondaryPorts.Models;

namespace Core.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateExpenseData, CreateExpenseEntity>();
        CreateMap<ExpenseEntity, ExpenseData>();
        CreateMap<ReportGenerationRequestData, ReportGenerationRequestEntity>();
    }
}
