using System.Data;

namespace PracticalChallenge.BookstoreManagement.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
