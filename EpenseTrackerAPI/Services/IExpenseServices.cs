using EpenseTrackerAPI.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace EpenseTrackerAPI.Services
{
    public interface IExpenseServices
    {
        Task<List<Expense>> GetAllExpensesAsync(int UserID);
        Task<Expense?> GetExpenseByIdAsync(int UserID, int id);
        Task<Expense?> AddExpense(int UserID, ExpenseDTO newExpense);
        Task<Expense?> UpdateExpense(int UserID, int id, ExpenseDTO UpdatedExpense);
        Task<bool> DeleteExpense(int UserID, int id);
    }
}
