namespace SecondaryPorts.Models;

public class ExpenseEntity
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Category { get; set; }

    public decimal Amount { get; set; }
    
    public DateTime Date { get; set; }

    public DateTime CreatedAt { get; set; }
}
