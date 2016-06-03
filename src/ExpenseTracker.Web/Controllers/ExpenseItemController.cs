using ExpenseTracker.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Data.Infrastructure;
using ExpenseTracker.Entities;
using ExpenseTracker.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Web.ViewModels;
using AutoMapper;

namespace ExpenseTracker.Web.Controllers
{
    public class ExpenseItemController : ExpenseTrackerCtrlBase
    {
        IEntityBaseRepository<ExpenseItem> _expenseRepository;

        
        public ExpenseItemController(IUnitofWork unitofWork, IEntityBaseRepository<ExpenseItem> expenseRepository) : base(unitofWork)
        {
            _expenseRepository = expenseRepository;
        }

        [HttpGet]
        public IEnumerable<ExpenseItem> GetExpenses(DateTime? purchaseddate)
        {
            return _expenseRepository.AllIncluding(item => item.Category, item => item.PaymentMethod, item => item.Biller)
                                     .Where(i => i.PurchasedDate.Date == (purchaseddate.HasValue ? purchaseddate.Value.Date : DateTime.Today))
                                     .AsEnumerable();
        }

        [HttpPost]
        public bool AddExpese([FromBody]ExpenseItemViewModel item)
        {
            bool result;
            if (item == null)
            {
                NoContent();
                result = false;
            }
            else
            {
                var model = Mapper.Map<ExpenseItem>(item);
                _expenseRepository.Add(model);
                _unitofWork.Commit();
                result = true;
            }
            return result;
        }

        [HttpPost]
        public bool EditExpese([FromBody]ExpenseItemViewModel item)
        {
            bool result;
            if (item == null)
            {
                NoContent();
                result = false;
            }
            else
            {
                var model = Mapper.Map<ExpenseItem>(item);
                _expenseRepository.Edit(model);
                _unitofWork.Commit();
                result = true;
            }
            return result;
        }
    }
}
