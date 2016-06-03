using AutoMapper;
using ExpenseTracker.Entities;
using ExpenseTracker.Web.ViewModels;

namespace ExpenseTracker.Web.Infrastructure.Mappings
{

    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Biller, BillerViewModel>();
            CreateMap<UserRole, RoleViewModal>()
                .ForMember(vm => vm.RoleId, map => map.MapFrom(m => m.RoleId))
                .ForMember(vm => vm.RoleName, map => map.MapFrom(m => m.Role.ID));

            CreateMap<User, UserRoleViewModel>()
                .ForMember(viewModel => viewModel.UserName, map => map.MapFrom(m => m.Username))
                .ForMember(viewModel => viewModel.Email, map => map.MapFrom(m => m.Email))
                .ForMember(viewModel => viewModel.IsLocked, map => map.MapFrom(m => m.IsLocked))
                .ForMember(viewModel => viewModel.UserRoles, map => map.MapFrom(m => m.UserRoles));

            CreateMap<PaymentMethod, PaymentMethodViewModel>()
                .ForMember(viewModel => viewModel.Id, map => map.MapFrom(m => m.ID))
                .ForMember(viewModel => viewModel.Name, map => map.MapFrom(m => m.Name));
        }
    }

   
}
