using MediatR;
using PracticalChallenge.BookstoreManagement.Domain.Abstractions;

namespace PracticalChallenge.BookstoreManagement.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
