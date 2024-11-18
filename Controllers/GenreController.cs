using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    public GenreController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllGenres()
    {
        var genres = _context.Genres.ToList();
        return Ok(genres);
    }

    [HttpGet("{genreId}")]
    public IActionResult GetGenreById([FromRoute] int genreId)
    {

        var genre = _context.Genres.Find(genreId);
        if(genre == null)
        {
            return NotFound();
        }
        return Ok(genre);
    }

}