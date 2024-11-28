using Microsoft.AspNetCore.Mvc;


[Route("api/anime")]
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
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var animes = await _animeRepo.GetAllAnimesAsync();
        
        var animeDto = animes.Select(s => s.ToAnimeDto());

        return Ok(animeDto);
    }

    [HttpGet("{animeId:int}")]
    public async Task<IActionResult> GetAnimeById([FromRoute] int animeId)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var anime = await _animeRepo.GetAnimeByIdAsync(animeId);

        if(anime == null)
        {
            return NotFound();
        }

        return Ok(anime.ToAnimeDto());
    }

    [HttpPost("{genreId:int}")]
    public async Task<IActionResult> CreateAnime([FromRoute] int genreId, CreateAnimeRequestDto animeDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if(!await _genreRepo.GenreExists(genreId))
        {
            return BadRequest("Genre does not exist");
        }

        var animeModel = animeDto.ToAnimeFromCreateDTO(genreId);

        await _animeRepo.CreateAnimeAsync(animeModel);

        return CreatedAtAction(nameof(GetAnimeById), new {animeId = animeModel.AnimeId}, animeModel.ToAnimeDto());
    }

    [HttpPut]
    [Route("{animeId:int}")]
    public async Task<IActionResult> UpdateAnime([FromRoute] int animeId, [FromBody] UpdateAnimeRequestDto updateAnimeDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var anime = await _animeRepo.UpdateAnimeAsync(animeId, updateAnimeDto.ToAnimeFromUpdateDTO());

        if(anime == null)
        {
            return NotFound("Anime not found");
        }

        return Ok(anime.ToAnimeDto());
    }

    [HttpDelete]
    [Route("{animeId:int}")]
    public async Task<IActionResult> DeleteAnime([FromRoute] int animeId)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var animeModel = await _animeRepo.DeleteAnimeAsync(animeId);

        if(animeModel == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
