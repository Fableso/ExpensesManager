using PrimaryPorts.Models;

namespace Core.Abstractions;

public interface IExpensesService
{
    Task<ExpenseData> CreateExpenseAsync(CreateExpenseData request);
    
    Task<IEnumerable<ExpenseData>> GetUserExpenses();
    
    Task<ExpenseData> GetExpenseByIdAsync(long id);
    
    Task DeleteExpenseAsync(long id);
}
