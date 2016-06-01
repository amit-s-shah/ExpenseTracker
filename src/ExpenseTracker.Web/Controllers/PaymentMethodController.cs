using ExpenseTracker.Web.Infrastructure.Core;
using System.Collections.Generic;
using ExpenseTracker.Data.Infrastructure;
using ExpenseTracker.Data.Repositories;
using ExpenseTracker.Entities;
using ExpenseTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ExpenseTracker.Web.Controllers
{
    public class PaymentMethodController : ExpenseTrackerCtrlBase
    {
        IEntityBaseRepository<PaymentMethod> _paymentMethodRepository;

        public PaymentMethodController(IUnitofWork unitofWork, IEntityBaseRepository<PaymentMethod> paymentMethodRepository) : base(unitofWork)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 1000)]
        public IEnumerable<PaymentMethodViewModel> GetAll()
        {
            var paymentMethods =_paymentMethodRepository.GetAll();
            return Mapper.Map<IEnumerable<PaymentMethodViewModel>>(paymentMethods);
        }
    }
}
