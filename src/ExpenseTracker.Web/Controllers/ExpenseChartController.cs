using System;
using System.Collections.Generic;
using System.Linq;
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

        public ExpenseChartViewModal<string, string, float> GetAllExpense(int? category)
        {
            ExpenseChartViewModal<string, string, float> modal = new ExpenseChartViewModal<string, string, float>();
            var expenses = _expenseRepository.AllIncluding(expense => expense.Category).Where(i => i.PurchasedDate > DateTime.Now.AddMonths(-6));
            if (category.HasValue && category.Value > 0)
            {
                var query = from expense in expenses
                            where expense.Category.ID == category.Value
                            group expense by
                            new
                            {
                                category = expense.Category.Name,
                                categoryId = expense.CategoryId,
                                ShortMonth = expense.PurchasedDate.ToShortMonthName() + "-" + expense.PurchasedDate.Year,
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
            }
            else
            {
                var query = from expense in expenses
                            group expense by
                            new
                            {
                                ShortMonth = expense.PurchasedDate.ToShortMonthName() + "-" + expense.PurchasedDate.Year,
                                month = expense.PurchasedDate.Month,
                            } into GrpByCatMonth
                            orderby GrpByCatMonth.Key.month
                            select new
                            {
                                GrpByCatMonth.Key.ShortMonth,
                                GrpAmount = GrpByCatMonth.Sum(a => a.Amount),
                                key = GrpByCatMonth.Key.ShortMonth
                            };
                var result = query.ToList();
                var dic = result.ToDictionary(i => i.key);
                modal.Labels = result.Select(i => i.ShortMonth).Distinct().ToList();
                modal.Series.Add("Total");
                foreach (var cat in modal.Series)
                {
                    modal.Data.Add(new List<float>());
                    foreach (var mnt in modal.Labels)
                    {
                        if (dic.ContainsKey(mnt))
                            modal.Data.Last().Add(dic[mnt].GrpAmount);
                        else
                            modal.Data.Last().Add(0);
                    }
                }
            }

            return modal;
        }

        public ExpenseChartViewModal<string, string, float> GetDataForPieChart()
        {
            var expenses = _expenseRepository.AllIncluding(expense => expense.Category).Where(i => i.PurchasedDate.Month == DateTime.Now.Month && i.PurchasedDate.Year == DateTime.Now.Year);
            var query = from expense in expenses
                        group expense by expense.Category.Name
                        into GrpByCat
                        orderby GrpByCat.Key
                        select new
                        {
                            name = GrpByCat.Key,
                            GrpAmount = GrpByCat.Sum(a => a.Amount),
                        };
            var result = query.OrderBy(e => e.GrpAmount).ToList();
            ExpenseChartViewModal<string, string, float> modal = new ExpenseChartViewModal<string, string, float>();
            modal.Data.Add(new List<float>());
            foreach (var item in result)
            {
                modal.Labels.Add(item.name);
                modal.Data.Last().Add(item.GrpAmount);
            }
            modal.Summary = "Total expnse in current month is: " + modal.Data.Sum(d => d.Sum(c => c));
            return modal;
        }

    }
}