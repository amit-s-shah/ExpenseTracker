using System;

namespace ExpenseTracker.Entities
{
    public class ExpenseItem : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public float Amount { get; set; }

        public DateTime PurchasedDate { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int BillerId { get; set; }

        public Biller Biller { get; set; }

        public int? InvoiceId { get; set; }

        public Invoice Invoice { get; set; }

        public int? PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        
    }
}
