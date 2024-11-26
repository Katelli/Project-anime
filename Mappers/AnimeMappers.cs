public static class AnimeMappers
{
    public static AnimeDto ToAnimeDto(this Anime animeModel)
    {
        return new AnimeDto
        {
            Name = animeModel.Name,
            AnimeId = animeModel.AnimeId,
            GenreId = animeModel.GenreId,
            Img = animeModel.Img,
            Url = animeModel.Url,
            Description = animeModel.Description
        };
    }
    public static Anime ToAnimeFromCreateDTO(this CreateAnimeRequestDto animeDto, int genreId)
    {
        return new Anime
        {
            Name = animeDto.Name,
            GenreId = genreId,
            Img = animeDto.Img,
            Url = animeDto.Url,
            Description = animeDto.Description
        };
    }
} 