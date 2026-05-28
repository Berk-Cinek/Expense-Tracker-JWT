using EpenseTrackerAPI.Entities.Models;
using EpenseTrackerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EpenseTrackerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController(IExpenseServices expenseService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Expense>>> GetExpenses()
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return Ok(await expenseService.GetAllExpensesAsync(userID));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpenseById(int id)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var expense = await expenseService.GetExpenseByIdAsync(userID, id);
            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> AddExpense(ExpenseDTO expenseDTO)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var expense = await expenseService.AddExpense(userID, expenseDTO);
            if(expense == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetExpenseById), new { id = expense.Id }, expense);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Expense>> UpdateExpense(ExpenseDTO expenseDTO, int id)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var expense = await expenseService.UpdateExpense(userID, id, expenseDTO);
            if( expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var expense = await expenseService.DeleteExpense(userID, id);
            if (!expense)
            {
                return NotFound();

            }
            return NoContent();
        }
    }
}
