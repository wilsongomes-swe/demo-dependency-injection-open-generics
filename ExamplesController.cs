using Microsoft.AspNetCore.Mvc;

namespace OpenGenericsDI;

public record Book(string Title, int Year);

public record Product(string Name, string Description);

public record Movie(string Title, string Synopsis);


[ApiController]
[Route("[controller]")]
public class ExamplesController : ControllerBase
{
    private readonly ILogger<ExamplesController> _logger;
    private readonly Book Book = new("DDD", 2003);
    private readonly Product Product = new("Computer", "Great computer");

    public ExamplesController(ILogger<ExamplesController> logger)
        => _logger = logger;

    [HttpGet("/Products")]
    public IActionResult GetProducts(ISerializer<Product> serializer) => Ok(new
    {
        Serialized = serializer.Serialize(Product)
    });

    [HttpGet("/Movies")]
    public IActionResult GetBooks(ISerializer<Movie> serializer) => Ok(new
    {
        Serialized = serializer.Serialize(new Movie("Movie", "Synopsis"))
    });

    [HttpGet("/Books")]
    public IActionResult GetBooks(ISerializer<Book> serializer) => Ok(new
    {
        Serialized = serializer.Serialize(Book)
    });
}