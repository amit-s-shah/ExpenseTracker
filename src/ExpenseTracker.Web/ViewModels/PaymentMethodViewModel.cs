using ExpenseTracker.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace ExpenseTracker.Web.ViewModels
{
    public class PaymentMethodViewModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
