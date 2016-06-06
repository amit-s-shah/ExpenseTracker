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

        public ExpenseChartViewModal<string, string, float> GetAllExpense()
        {
            var expenses = _expenseRepository.AllIncluding(expense => expense.Category).Where(i => i.PurchasedDate > DateTime.Now.AddMonths(-6));
            var query = from expense in expenses
                        group expense by
                        new
                        {
                            category = expense.Category.Name,
                            categoryId = expense.CategoryId,
                            ShortMonth = expense.PurchasedDate.ToShortMonthName() + "-"+ expense.PurchasedDate.Year,
                            month = expense.PurchasedDate.Month,
                        } into GrpByCatMonth
                        orderby GrpByCatMonth.Key.month, GrpByCatMonth.Key.categoryId
                        select new
                        {
                            GrpByCatMonth.Key.ShortMonth,
                            GrpByCatMonth.Key.category,
                            GrpAmount = GrpByCatMonth.Sum(a => a.Amount),
                            key = GrpByCatMonth.Key.ShortMonth + "_" + GrpByCatMonth.Key.category
                        };
            var result = query.ToList();
            var dic = result.ToDictionary(i => i.key);
            ExpenseChartViewModal<string, string, float> modal = new ExpenseChartViewModal<string, string, float>();
            modal.Labels = result.Select(i => i.ShortMonth).Distinct().ToList();
            modal.Series = result.Select(i => i.category).Distinct().ToList();
            foreach (var cat in modal.Series)
            {
                modal.Data.Add(new List<float>());
                foreach (var mnt in modal.Labels)
                {
                    if (dic.ContainsKey(mnt + "_" + cat))
                        modal.Data.Last().Add(dic[mnt + "_" + cat].GrpAmount);
                    else
                        modal.Data.Last().Add(0);
                }
            }
            return modal;
        }
    }
}
