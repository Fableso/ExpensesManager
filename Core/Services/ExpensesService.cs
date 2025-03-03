using AutoMapper;

using Core.Abstractions;

using Microsoft.AspNetCore.Http;

using PrimaryPorts.Models;

using SecondaryPorts.Abstractions;
using SecondaryPorts.Models;

namespace Core.Services;

public class ExpensesService : BaseService, IExpensesService
{
    private readonly IMapper _mapper;
    private readonly IExpensesProvider _expensesProvider;
    private readonly long _userId;

    public ExpensesService(
        IHttpContextAccessor httpContextAccessor,
        IExpensesProvider expensesProvider,
        IMapper mapper) : base(httpContextAccessor)
    {
        _expensesProvider = expensesProvider;
        _mapper = mapper;
        _userId = GetUserId();
    }
    
    public async Task<ExpenseData> CreateExpenseAsync(CreateExpenseData request)
    {
        var createExpenseEntity = _mapper.Map<CreateExpenseEntity>(request);
        
        createExpenseEntity.UserId = _userId;
        
        var createdExpense = await _expensesProvider.CreateExpenseAsync(createExpenseEntity);
        
        return _mapper.Map<ExpenseData>(createdExpense);
    }

    public async Task<IEnumerable<ExpenseData>> GetUserExpenses()
    {
        var expenses = await _expensesProvider.GetExpensesByUserIdAsync(_userId);
        
        return _mapper.Map<IEnumerable<ExpenseData>>(expenses);
    }

    public async Task<ExpenseData> GetExpenseByIdAsync(long id)
    {
        var expense = await _expensesProvider.GetExpenseAsync(id);

        if (expense.UserId != _userId)
        {
            throw new UnauthorizedAccessException("You are not allowed to access this expense.");
        }
        
        return _mapper.Map<ExpenseData>(expense);
    }

    public async Task DeleteExpenseAsync(long id)
    {
        var expense = await _expensesProvider.GetExpenseAsync(id);
        
        if (expense.UserId != _userId)
        {
            throw new UnauthorizedAccessException("You are not allowed to delete this expense.");
        }
        
        await _expensesProvider.DeleteExpenseAsync(id);
    }
}
