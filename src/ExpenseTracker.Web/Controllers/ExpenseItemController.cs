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
        public IEnumerable<ExpenseItem> GetExpenses(DateTime? purchasedate)
        {
            return _expenseRepository.FindBy(i => i.PurchasedDate == (purchasedate.HasValue ? purchasedate : DateTime.Today)).AsEnumerable();
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
    }
}
