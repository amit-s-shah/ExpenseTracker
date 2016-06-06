using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Web.ViewModels
{
    public class ExpenseChartViewModal<L, S, D>
    {
        //Y-axis values
        public IList<L> Labels { get; set; }

        //X-axis labels
        public IList<S> Series { get; set; }

        //labels on a chart area
        public IList<IList<D>> Data { get; set; }

        public ExpenseChartViewModal()
        {
            Labels = new List<L>();
            Series = new List<S>();
            Data = new List<IList<D>>();
        }
    }
}
