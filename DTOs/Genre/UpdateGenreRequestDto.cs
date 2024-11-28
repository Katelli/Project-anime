using System.ComponentModel.DataAnnotations;

public class UpdateGenreRequestDto
{
    [Required]
    [MinLength(4, ErrorMessage = "Name must contain at least 4 characters")]
    [MaxLength (280, ErrorMessage = "Name can not be over 280 characters")]
    public string GenreName { get; set; } = string.Empty;
}