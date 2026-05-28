using EpenseTrackerAPI.Data;
using EpenseTrackerAPI.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace EpenseTrackerAPI.Services
{
    public class ExpenseServices(AppDbContext context) : IExpenseServices
    {
        public async Task<Expense?> AddExpense(int UserID, ExpenseDTO newExpense)
        {
            if(newExpense == null)
            {
                return null;
            }

            var expense = new Expense()
            {
                UserID = UserID,
                Description = newExpense.Description,
                Date = newExpense.Date,
                expenseCatagorie = newExpense.expenseCatagorie,
                Amount = newExpense.Amount
            };

            context.Expenses.Add(expense);
            await context.SaveChangesAsync();
            return (expense);
        }

        public async Task<bool> DeleteExpense(int UserID, int id)
        {
            var expense = await context.Expenses
                .FirstOrDefaultAsync(e => e.UserID == UserID && e.Id == id);
            if (expense == null)
            {
                return false;
            }
            context.Expenses.Remove(expense);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<List<Expense>> GetAllExpensesAsync(int UserID)
        {
            var expense = await context.Expenses
                .Where(e => e.UserID == UserID).ToListAsync();
            return expense;
        }

        public async Task<Expense?> GetExpenseByIdAsync(int UserID, int id)
        {
            var expense = await context.Expenses.FirstOrDefaultAsync(e => e.Id == id && e.UserID == UserID);
            return expense;
            
        }

        public async Task<Expense?> UpdateExpense(int UserID, int id, ExpenseDTO UpdatedExpense)
        {
            var expense = await context.Expenses.FirstOrDefaultAsync(e => e.Id == id && e.UserID == UserID);
            if (expense == null)
            {
                return expense;
            }
            
            expense.Amount = UpdatedExpense.Amount;
            expense.Description = UpdatedExpense.Description;
            expense.expenseCatagorie = UpdatedExpense.expenseCatagorie;
            expense.Date = UpdatedExpense.Date;

            await context.SaveChangesAsync();

            return expense;
        }
    }
}
