using SecondaryPorts.Models;

namespace SecondaryPorts.Abstractions;

public interface IExpensesProvider
{
    Task<ExpenseEntity> CreateExpenseAsync(CreateExpenseEntity entity);
    
    Task<IEnumerable<ExpenseEntity>> GetExpensesByUserIdAsync(long userId);
    
    Task<ExpenseEntity> GetExpenseAsync(long id);
    
    Task DeleteExpenseAsync(long id);
}
