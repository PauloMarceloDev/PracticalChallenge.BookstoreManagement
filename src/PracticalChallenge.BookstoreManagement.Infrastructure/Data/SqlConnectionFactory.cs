using System.Data;
using Microsoft.Data.Sqlite;
using PracticalChallenge.BookstoreManagement.Application.Abstractions.Data;

namespace PracticalChallenge.BookstoreManagement.Infrastructure.Data;

internal sealed class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        var connection = new SqliteConnection(connectionString);
        connection.Open();

        return connection;
    }
}
