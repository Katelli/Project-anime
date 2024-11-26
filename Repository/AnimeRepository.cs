using Microsoft.EntityFrameworkCore;
public class AnimeRepository : IAnimeRepository
{
    private readonly ApplicationDBContext _context;
    public AnimeRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Anime> CreateAnimeAsync(Anime animeModel)
    {
        await _context.Animes.AddAsync(animeModel);
        await _context.SaveChangesAsync();
        return animeModel;
    }

    public async Task<Anime?> DeleteAnimeAsync(int animeId)
    {
        var animeModel = await _context.Animes.FirstOrDefaultAsync(x => x.AnimeId == animeId);

        if(animeModel == null)
        {
            return null;
        }

        _context.Animes.Remove(animeModel);
        await _context.SaveChangesAsync();

        return animeModel;
    }

    public async Task<List<Anime>> GetAllAnimesAsync()
    {
        return await _context.Animes.ToListAsync();
    }

    public async Task<Anime?> GetAnimeByIdAsync(int animeId)
    {
        return await _context.Animes.FindAsync(animeId);
    }

    public async Task<Anime?> UpdateAnimeAsync(int animeId, Anime animeModel)
    {
        var existingAnime = await _context.Animes.FindAsync(animeId);

        if(existingAnime == null)
        {
            return null;
        }

        existingAnime.Name = animeModel.Name;
        existingAnime.Img = animeModel.Img;
        existingAnime.Url = animeModel.Url;
        existingAnime.Description = animeModel.Description;
        
        await _context.SaveChangesAsync();

        return existingAnime;
    }
}