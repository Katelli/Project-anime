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
        var genres = _context.Genres.ToList()
        .Select(s => s.ToGenreDto());
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
        return Ok(genre.ToGenreDto());
    }

    [HttpPost]
    public IActionResult CreateGenre([FromBody] CreateGenreRequestDto genreDto)
    {
        var genreModel = genreDto.ToGenreFromCreateDTO();
        _context.Genres.Add(genreModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetGenreById), new {genreId = genreModel.GenreId}, genreModel.ToGenreDto());
    }

    [HttpPut]
    [Route("{genreId}")]
    public IActionResult UpdateGenre([FromRoute] int genreId, [FromBody] UpdateGenreRequestDto updateGenreDto)
    {
        var genreModel = _context.Genres.FirstOrDefault(x => x.GenreId == genreId);

        if(genreModel == null)
        {
            return NotFound();
        }

        genreModel.GenreName = updateGenreDto.GenreName;
        _context.SaveChanges();
        return Ok(genreModel.ToGenreDto());
    }

    [HttpDelete]
    [Route("{genreId}")]
    public IActionResult DeleteGenre([FromRoute] int genreId)
    {
        var genreModel = _context.Genres.FirstOrDefault(x => x.GenreId == genreId);

        if(genreModel == null)
        {
            return NotFound();
        }

        _context.Genres.Remove(genreModel);
        _context.SaveChanges();
        return NoContent();
    }

}