using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class AnimeController : ControllerBase
{
    private readonly IAnimeRepository _animeRepo;
    private readonly IGenreRepository _genreRepo;
    public AnimeController(IAnimeRepository animeRepo, IGenreRepository genreRepo)
    {
        _animeRepo = animeRepo;
        _genreRepo = genreRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAnimes()
    {
        var animes = await _animeRepo.GetAllAnimesAsync();
        
        var animeDto = animes.Select(s => s.ToAnimeDto());

        return Ok(animeDto);
    }

    [HttpGet("{animeId}")]
    public async Task<IActionResult> GetAnimeById([FromRoute] int animeId)
    {
        var anime = await _animeRepo.GetAnimeByIdAsync(animeId);

        if(anime == null)
        {
            return NotFound();
        }

        return Ok(anime.ToAnimeDto());
    }

    [HttpPost("{genreId}")]
    public async Task<IActionResult> CreateAnime([FromRoute] int genreId, CreateAnimeRequestDto animeDto)
    {
        if(!await _genreRepo.GenreExists(genreId))
        {
            return BadRequest("Genre does not exist");
        }

        var animeModel = animeDto.ToAnimeFromCreateDTO(genreId);

        await _animeRepo.CreateAnimeAsync(animeModel);

        return CreatedAtAction(nameof(GetAnimeById), new {animeId = animeModel.AnimeId}, animeModel.ToAnimeDto());
    }
}
