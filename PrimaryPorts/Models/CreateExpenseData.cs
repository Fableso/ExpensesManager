namespace PrimaryPorts.Models;

public class CreateExpenseData
{
    public string Category { get; set; }

    public decimal Amount { get; set; }
    
    public DateTime Date { get; set; }
}
