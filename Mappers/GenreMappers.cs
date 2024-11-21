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
}