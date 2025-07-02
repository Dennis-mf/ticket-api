using Oracle.ManagedDataAccess.Client;

namespace ticket_api.Database;
public class OracleDbService
{
    private readonly string _connectionString;

    public OracleDbService (IConfiguration configuration)
    {
        //todo: check this warning
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    //get db connection 
    public OracleConnection GetConnection()
    {
        return new OracleConnection(_connectionString);
    }
}
