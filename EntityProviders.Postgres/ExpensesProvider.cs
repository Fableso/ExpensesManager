using AutoMapper;

using EntityProviders.Postgres.Entities;

using Microsoft.EntityFrameworkCore;

using SecondaryPorts.Abstractions;
using SecondaryPorts.Exceptions;
using SecondaryPorts.Models;

namespace EntityProviders.Postgres;

public class ExpensesProvider : IExpensesProvider
{
    private readonly IMapper _mapper;
    private readonly ExpensesDbContext _context;
    
    public ExpensesProvider(
        IMapper mapper,
        ExpensesDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<ExpenseEntity> CreateExpenseAsync(CreateExpenseEntity entity)
    {
        var expense = _mapper.Map<Expense>(entity);
        
        await _context.Expenses.AddAsync(expense);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ExpenseEntity>(expense);
    }
    
    public async Task<IEnumerable<ExpenseEntity>> GetExpensesByUserIdAsync(long userId)
    {
        var expenses = await _context.Expenses
            .Where(e => e.UserId == userId)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<ExpenseEntity>>(expenses);
    }
    
    public async Task<ExpenseEntity> GetExpenseAsync(long id)
    {
        var expense = await _context.Expenses
            .FirstOrDefaultAsync(e => e.Id == id);
        
        if (expense is null)
        {
            throw new ExpenseNotFoundException($"Expense with id {id} is not found");
        }
        
        return _mapper.Map<ExpenseEntity>(expense);
    }
    
    public async Task DeleteExpenseAsync(long id)
    {
        var expense = await _context.Expenses
            .FirstOrDefaultAsync(e => e.Id == id);

        if (expense is null)
        {
            throw new ExpenseNotFoundException($"Expense with id {id} is not found");
        }

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }
}
