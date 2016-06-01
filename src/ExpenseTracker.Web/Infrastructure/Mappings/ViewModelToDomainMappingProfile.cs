using AutoMapper;
using ExpenseTracker.Entities;
using ExpenseTracker.Web.ViewModels;

namespace ExpenseTracker.Web.Infrastructure.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<BillerViewModel, Biller>();
            CreateMap<ExpenseItemViewModel, ExpenseItem>();
        }
    }
}
