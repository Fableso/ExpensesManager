using Core.Abstractions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using PrimaryPorts.Models;

using SecondaryPorts.Exceptions;

namespace ExpenseManagement.TransactionEntities.Controllers;

public class ExpensesController : BaseController
{
    private readonly IExpensesService _expensesService;

    public ExpensesController(
        IExpensesService expensesService)
    {
        _expensesService = expensesService;
    }
    
    [HttpPost("create")]
    [ProducesResponseType(200, Type = typeof(ExpenseData))]
    [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> CreateExpenseAsync([FromBody] CreateExpenseData request)
    {
        var result = await _expensesService.CreateExpenseAsync(request);
        
        return Ok(result);
    }
    
    [HttpGet("all")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseData>))]
    public async Task<IActionResult> GetExpensesByUserIdAsync()
    {
        var result =  await _expensesService.GetUserExpenses();
        
        return Ok(result);
    }
    
    [HttpGet("{id}", Name = "Expenses_GetById")]
    [ProducesResponseType(200, Type = typeof(ExpenseData))]
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> GetExpenseByIdAsync(long id)
    {
        try
        {
            var result = await _expensesService.GetExpenseByIdAsync(id);
            
            return Ok(result);
        }
        catch (ExpenseNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> DeleteExpenseAsync(long id)
    {
        try
        {
            await _expensesService.DeleteExpenseAsync(id);
            
            return NoContent();
        }
        catch (ExpenseNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}
