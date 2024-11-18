public class Anime
{
    public string Name { get; set; } = string.Empty;
    public int? GenreId { get; set; }
    public Genre? genre;
    public int AnimeId { get; set; }

    //Need to find out what I should put here
    public string Url { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}