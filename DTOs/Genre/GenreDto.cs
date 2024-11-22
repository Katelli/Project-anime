public class GenreDto
{
    public string? GenreName { get; set; } = string.Empty;
    public int GenreId { get; set; }
    public List<AnimeDto> Animes { get; set; }
}