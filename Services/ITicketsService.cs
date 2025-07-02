using ticket_api.Models;

public interface ITicketsService
{
    Task<List<Ticket>> GetTickets(int userId);
}