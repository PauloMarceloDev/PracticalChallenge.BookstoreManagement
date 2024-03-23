using MediatR;
using Microsoft.AspNetCore.Mvc;
using PracticalChallenge.BookstoreManagement.Application.Books.AddBook;
using PracticalChallenge.BookstoreManagement.Application.Books.DeleteBook;
using PracticalChallenge.BookstoreManagement.Application.Books.GetAllBooks;
using PracticalChallenge.BookstoreManagement.Application.Books.GetBookById;
using PracticalChallenge.BookstoreManagement.Application.Books.UpdateBook;
using PracticalChallenge.BookstoreManagement.Domain.Abstractions;

namespace PracticalChallenge.BookstoreManagement.Api.Controllers.Books;

[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[Route("books")]
public class BooksController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookResponse>>> GetBooks(CancellationToken cancellationToken)
    {
        Result<IEnumerable<BookResponse>> result = await sender.Send(new GetAllBooksQuery(), cancellationToken);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
    
    
    [HttpGet("{bookId:guid}")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<BookResponse>> GetBookById(Guid bookId, CancellationToken cancellationToken)
    {
        Result<BookResponse> result = await sender.Send(new GetBookByIdQuery(bookId), cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return result.Value;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddBook(AddBookRequest request, CancellationToken cancellationToken)
    {
        var command = new AddBookCommand(request.Title,
            request.Authors,
            request.Genres,
            request.MoneyCurrency,
            request.MoneyValue,
            request.QuantityInStock);

        Result<Guid> result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        Result<BookResponse> getBookByIdResult = await sender.Send(new GetBookByIdQuery(result.Value), cancellationToken);

        return CreatedAtAction(nameof(GetBookById), new { bookId = result.Value }, getBookByIdResult.Value);
    }
    
    [HttpPut("{bookId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBook(Guid bookId, UpdateBookRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateBookCommand(
            bookId,
            request.Title,
            request.Authors,
            request.Genres,
            request.MoneyCurrency,
            request.MoneyValue,
            request.QuantityInStock);

        Result result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
    
    [HttpDelete("{bookId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook(Guid bookId, CancellationToken cancellationToken)
    {
        Result result = await sender.Send(new DeleteBookCommand(bookId), cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        
        return NoContent();
    }
}
