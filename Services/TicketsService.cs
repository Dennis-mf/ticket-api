using ticket_api.Models;

namespace ticket_api.Services;

public class TicketsService: ITicketsService
{
    private ITicketDao _ticketDao;

    public TicketsService(ITicketDao ticketDao)
    {   
        _ticketDao = ticketDao;
    }

    public async Task<List<Ticket>> GetTickets(int userId)
    {
        var tickets = await _ticketDao.GetTickets(userId);
        return tickets.ToList();
    }

}