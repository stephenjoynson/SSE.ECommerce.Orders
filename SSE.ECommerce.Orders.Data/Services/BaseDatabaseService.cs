using System.Data;

namespace SSE.ECommerce.Orders.Data.Services
{
    public class BaseDatabaseService
    {
        protected IDbConnection DbConnection { get; }

        protected BaseDatabaseService(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        protected void EnsureDbConnectionIsOpen()
        {
            if (DbConnection.State != ConnectionState.Open)
            { DbConnection.Open(); }

        }

        protected void EnsureDbConnectionIsClosed()
        {
            if (DbConnection.State != ConnectionState.Closed)
            { DbConnection.Close(); }
        }
    }
}
