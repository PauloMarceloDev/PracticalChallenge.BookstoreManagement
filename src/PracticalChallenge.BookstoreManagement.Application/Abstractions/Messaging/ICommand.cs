using MediatR;
using PracticalChallenge.BookstoreManagement.Domain.Abstractions;

namespace PracticalChallenge.BookstoreManagement.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}
