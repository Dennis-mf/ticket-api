using Oracle.ManagedDataAccess.Client;
using ticket_api.Database;
using ticket_api.Models;
namespace ticket_api.DAO;

public class TicketDao : ITicketDao
{
    private readonly OracleDbService _dbService;

    public TicketDao(OracleDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<List<Ticket>> GetTickets(int userId)
    {
        using var connection = _dbService.GetConnection();
        var tickets = new List<Ticket>();

        //basic, then create the joins to get the correct names and descriptions for the foreign columns 
        using var command = new OracleCommand("SELECT id, title, user_assigned_id, created_by_id, urgency_level_id, state_id FROM tickets WHERE user_assigned_id = :id", connection);

        command.Parameters.Add(new OracleParameter("id", userId));
        try{
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var ticket = new Ticket(){
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    UserAssigned = reader.GetString(2),
                    CreatedBy = reader.GetString(3),
                    UrgencyLevel = reader.GetString(4),
                    State = reader.GetString(5)
                };

                tickets.Add(ticket);
            }
        }
        catch(Exception ex)
        {
            throw;
        }

        return tickets;
    }
}