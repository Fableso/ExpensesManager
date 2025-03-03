using AutoMapper;

using EntityProviders.Postgres.Entities;

using SecondaryPorts.Models;

namespace EntityProviders.Postgres.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ExpenseEntity, Expense>().ReverseMap();
        CreateMap<CreateExpenseEntity, Expense>();
    }
}
