namespace ExpenseTracker.Entities
{
    public class Invoice : EntityBase
    {
        public byte[] InvoiceCopy { get; set; }

        public string Format { get; set; }
    }
}
