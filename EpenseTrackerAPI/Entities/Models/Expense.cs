using System.Text.Json.Serialization;

namespace EpenseTrackerAPI.Entities.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public String Description { get; set; } = String.Empty;
        public Decimal Amount { get; set; }
        public DateTime Date {  get; set; }
        public int UserID { get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!;
        public ExpenseCatagorie expenseCatagorie { get; set; }

    }

    public enum ExpenseCatagorie { 
        Groceries,
        Lesiure,
        Electronics,
        Utilities,
        Clothing,
        Health,
        Others
    }
}
