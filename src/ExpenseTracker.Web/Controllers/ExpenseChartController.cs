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

        /// <summary>
        /// Month, category, amonth
        /// </summary>
        /// <returns></returns>
        public ExpenseChartViewModal<string, string, float> GetAllExpense()
        {
            var result = new ExpenseChartViewModal<string, string, float>();
            var expenses = _expenseRepository.AllIncluding(item => item.Category).ToList();

            var query = from exp in expenses
                        group exp by exp.PurchasedDate.ToShortMonthName() into mnthGrp
                        from mnthCatGrp in
                            (
                                from exp in mnthGrp
                                group exp by exp.Category.Name
                            )
                        group mnthCatGrp by mnthGrp.Key
                        ;

            foreach (var outerGroup in query)
            {
                result.Labels.Add(outerGroup.Key);
                result.Data.Add(new List<float>());
                foreach (var innerGroup in outerGroup)
                {
                    result.Series.Add(innerGroup.Key);
                    result.Data[result.Labels.Count - 1].Add(innerGroup.Sum(i => i.Amount));
                }
            }
            return result;
        }
    }
}
