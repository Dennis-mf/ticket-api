namespace ticket_api.Models;

public class Ticket 
{
    public int Id { get; set; } = 0;
    public string Title { get; set; } = "";
    public string Description { get; set;} = "";
    public string UserAssigned { get; set; } = "";
    public string CreatedBy { get; set; } = "";
    public string CreationDate { get; set; } = "";
    public string UrgencyLevel {get; set;} = "";
    public string State { get; set; } = "";
}