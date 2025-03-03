namespace SecondaryPorts.Models;

public class CreateExpenseEntity
{
    public long UserId { get; set; }

    public string Category { get; set; }

    public decimal Amount { get; set; }
    
    public DateTime Date { get; set; }
}
