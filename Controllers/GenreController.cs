using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly IGenreRepository _genreRepo;
    public GenreController(ApplicationDBContext context, IGenreRepository genreRepo)
    {
        _genreRepo = genreRepo;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        var genres = await _genreRepo.GetAllGenresAsync();
        
        var genreDto = genres.Select(s => s.ToGenreDto());

        return Ok(genreDto);
    }

    [HttpGet("{genreId}")]
    public async Task<IActionResult> GetGenreById([FromRoute] int genreId)
    {

        var genre = await _genreRepo.GetGenreByIdAsync(genreId);

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

        await _genreRepo.CreateGenreAsync(genreModel);

        return CreatedAtAction(nameof(GetGenreById), new {genreId = genreModel.GenreId}, genreModel.ToGenreDto());
    }

    [HttpPut]
    [Route("{genreId}")]
    public async Task<IActionResult> UpdateGenre([FromRoute] int genreId, [FromBody] UpdateGenreRequestDto updateGenreDto)
    {
        var genreModel = await _genreRepo.UpdateGenreAsync(genreId, updateGenreDto);

        if(genreModel == null)
        {
            return NotFound();
        }

        return Ok(genreModel.ToGenreDto());
    }

    [HttpDelete]
    [Route("{genreId}")]
    public async Task<IActionResult> DeleteGenre([FromRoute] int genreId)
    {
        var genreModel = await _genreRepo.DeleteGenreAsync(genreId);

        if(genreModel == null)
        {
            return NotFound();
        }

        return NoContent();
    }

}