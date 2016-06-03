using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Web.Infrastructure.Core;
using ExpenseTracker.Data.Infrastructure;
using ExpenseTracker.Entities;
using ExpenseTracker.Data.Repositories;
using ExpenseTracker.Web.ViewModels;
using ExpenseTracker.Web.Infrastructure.Extensions;

namespace ExpenseTracker.Web.Controllers
{
    public class ExpenseChartController : ExpenseTrackerCtrlBase
    {
        IEntityBaseRepository<ExpenseItem> _expenseRepository;

        public ExpenseChartController(IEntityBaseRepository<ExpenseItem> expenseRepository, IUnitofWork unitofWork) : base(unitofWork)
        {
            _expenseRepository = expenseRepository;
        }

        //Type of Grap
        //Y-axis X-axis
        //Amount vs Month
        //Amount per category vs Month

        public void GetAllExpense()
        {
            ExpenseChartViewModal expenseChartViewModal = new ExpenseChartViewModal();
            expenseChartViewModal.Amounts = new List<List<float>>();
            expenseChartViewModal.Categories = new List<List<string>>();

            var expenses = _expenseRepository.GetAll();
            expenseChartViewModal.Amounts.Append(expenses.Select(e => e.Amount).ToList());
            expenseChartViewModal.Months = expenses.Select(e => e.PurchasedDate.ToShortMonthName()).AsEnumerable();
        }
    }
}
