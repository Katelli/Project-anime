using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> GetAllGenres()
    {
        var genres = await _context.Genres.ToListAsync();
        
        var genreDto = genres.Select(s => s.ToGenreDto());
        return Ok(genreDto);
    }

    [HttpGet("{genreId}")]
    public async Task<IActionResult> GetGenreById([FromRoute] int genreId)
    {

        var genre = await _context.Genres.FindAsync(genreId);
        if(genre == null)
        {
            return NotFound();
        }
        return Ok(genre.ToGenreDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenre([FromBody] CreateGenreRequestDto genreDto)
    {
        var genreModel = genreDto.ToGenreFromCreateDTO();
        await _context.Genres.AddAsync(genreModel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetGenreById), new {genreId = genreModel.GenreId}, genreModel.ToGenreDto());
    }

    [HttpPut]
    [Route("{genreId}")]
    public async Task<IActionResult> UpdateGenre([FromRoute] int genreId, [FromBody] UpdateGenreRequestDto updateGenreDto)
    {
        var genreModel = await _context.Genres.FirstOrDefaultAsync(x => x.GenreId == genreId);

        if(genreModel == null)
        {
            return NotFound();
        }

        genreModel.GenreName = updateGenreDto.GenreName;
        await _context.SaveChangesAsync();
        return Ok(genreModel.ToGenreDto());
    }

    [HttpDelete]
    [Route("{genreId}")]
    public async Task<IActionResult> DeleteGenre([FromRoute] int genreId)
    {
        var genreModel = await _context.Genres.FirstOrDefaultAsync(x => x.GenreId == genreId);

        if(genreModel == null)
        {
            return NotFound();
        }

        _context.Genres.Remove(genreModel);
        await _context.SaveChangesAsync();
        return NoContent();
    }

}