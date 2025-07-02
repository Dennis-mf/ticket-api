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
        
        var queryString = @"SELECT tickets.id, tickets.title, users.name AS user_assigned, users2.name AS created_by, urgency_levels.description, ticket_status.description
                    FROM tickets 
                    JOIN users ON users.id = tickets.user_assigned_id
                    JOIN users users2 ON users2.id = tickets.created_by_id
                    JOIN urgency_levels ON urgency_levels.id = tickets.urgency_level_id
                    JOIN ticket_status ON ticket_status.id = tickets.state_id
                    WHERE tickets.user_assigned_id = :id";

        using var command = new OracleCommand(queryString, connection);

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