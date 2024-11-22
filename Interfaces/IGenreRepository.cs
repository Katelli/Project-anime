public interface IGenreRepository
{
    Task<List<Genre>> GetAllGenresAsync();
    // We write it as Genre? because FirstOrDefault can be null
    Task<Genre?> GetGenreByIdAsync(int genreId);
    Task<Genre> CreateGenreAsync(Genre genreModel);
    Task<Genre?> UpdateGenreAsync(int genreId, UpdateGenreRequestDto genreDto);
    Task<Genre?> DeleteGenreAsync(int genreId);
}