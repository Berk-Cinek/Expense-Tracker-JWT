namespace EpenseTrackerAPI.Entities.Models
{
    public class ExpenseDTO
    {
        public String Description { get; set; } = String.Empty;
        public Decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int UserID { get; set; }
        public ExpenseCatagorie expenseCatagorie { get; set; }
    }
}
