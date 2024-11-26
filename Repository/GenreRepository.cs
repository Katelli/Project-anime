using Microsoft.EntityFrameworkCore;
public class GenreRepository : IGenreRepository
{
    private readonly ApplicationDBContext _context;
    public GenreRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Genre> CreateGenreAsync(Genre genreModel)
    {
        await _context.Genres.AddAsync(genreModel);
        await _context.SaveChangesAsync();
        return genreModel;
    }

    public async Task<Genre?> DeleteGenreAsync(int genreId)
    {
        var genreModel = await _context.Genres.FirstOrDefaultAsync(x => x.GenreId == genreId);

        if(genreModel == null)
        {
            return null;
        }

        _context.Genres.Remove(genreModel);
        await _context.SaveChangesAsync();

        return genreModel;
    }

    public Task<bool> GenreExists(int genreId)
    {
        return _context.Genres.AnyAsync(g => g.GenreId == genreId);
    }

    public async Task<List<Genre>> GetAllGenresAsync()
    {
        return await _context.Genres.Include(a => a.Animes).ToListAsync();
    }

    public async Task<Genre?> GetGenreByIdAsync(int genreId)
    {
        return await _context.Genres.Include(a => a.Animes).FirstOrDefaultAsync(i => i.GenreId == genreId);
    }

    public async Task<Genre?> UpdateGenreAsync(int genreId, UpdateGenreRequestDto genreDto)
    {
        var existingGenre = await _context.Genres.FirstOrDefaultAsync(x => x.GenreId == genreId);

        if(existingGenre == null)
        {
            return null;
        }

        existingGenre.GenreName = genreDto.GenreName;
        
        await _context.SaveChangesAsync();

        return existingGenre;
    }
}