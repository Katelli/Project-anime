public interface IAnimeRepository
{
    Task<List<Anime>> GetAllAnimesAsync();
    Task<Anime?> GetAnimeByIdAsync(int animeId);
    // Task<Anime> CreateAnimeAsync(Anime animeModel);
    // Task<Anime?> UpdateAnimeAsync(int animeId, UpdateAnimeRequestDto animeDto);
    // Task<Anime?> DeleteAnimeAsync(int animeId);
}