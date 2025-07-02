using ticket_api.Models;

public interface ITicketDao
{
    Task<List<Ticket>> GetTickets(int userId);

}