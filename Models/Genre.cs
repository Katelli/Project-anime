public class Genre
{
    public string? GenreName { get; set; } = string.Empty;
    public int GenreId { get; set; }

    //Need to find out what I should put here
    public List<Anime> Animes { get; set; } = new List<Anime>();
}