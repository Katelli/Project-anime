public static class GenreMappers
{
    public static GenreDto ToGenreDto(this Genre genreModel)
    {
        return new GenreDto
        {
            GenreId = genreModel.GenreId,
            GenreName = genreModel.GenreName
        };
    }

    public static Genre ToGenreFromCreateDTO(this CreateGenreRequestDto genreDto)
    {
        return new Genre
        {
            GenreName = genreDto.GenreName
        };
    }

}