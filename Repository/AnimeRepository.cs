using Microsoft.EntityFrameworkCore;
public class AnimeRepository : IAnimeRepository
{
    private readonly ApplicationDBContext _context;
    public AnimeRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Anime>> GetAllAnimesAsync()
    {
        return await _context.Animes.ToListAsync();
    }

    public async Task<Anime?> GetAnimeByIdAsync(int animeId)
    {
        return await _context.Animes.FindAsync(animeId);
    }
}