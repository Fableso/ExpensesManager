namespace SecondaryPorts.Models;

public class ReportGenerationRequestEntity
{
    public long UserId { get; set; }
    
    public string Email { get; set; }
    
    public DateTime From { get; set; }

    public DateTime To { get; set; }
}
