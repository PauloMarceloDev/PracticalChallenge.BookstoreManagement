using MediatR;
using PracticalChallenge.BookstoreManagement.Domain.Abstractions;

namespace PracticalChallenge.BookstoreManagement.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
