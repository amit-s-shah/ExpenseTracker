using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Web.ViewModels
{
    public class ExpenseChartViewModal
    {
        //Y-axis values
        public IEnumerable<IEnumerable<float>> Amounts { get; set; }

        //X-axis labels
        public IEnumerable<string> Months { get; set; }

        //labels on a chart area
        public IEnumerable<IEnumerable<string>> Categories { get; set; }

        //labels on a chart area
        public IEnumerable<IEnumerable<string>> Billers { get; set; }

    }
}
