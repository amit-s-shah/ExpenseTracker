using System.Collections.Generic;

namespace ExpenseTracker.Web.ViewModels
{
    public class ExpenseChartViewModal<L,S, D>
    {
        public List<L> Labels { get; set; }

        public List<S> Series { get; set; }

        //data for chart
        // number of Series must be same as number of arrays in Data
        // number of elements in each array must be same as number of elements of Labels 
        public List<List<D>> Data { get; set; }

        public ExpenseChartViewModal()
        {
            Labels = new List<L>();
            Series = new List<S>();
            Data = new List<List<D>>();
        }
    }
}
