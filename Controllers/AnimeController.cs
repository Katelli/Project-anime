using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class AnimeController : ControllerBase
{
    private readonly IAnimeRepository _animeRepo;
    public AnimeController(IAnimeRepository animeRepo)
    {
        _animeRepo = animeRepo;
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
}
