using System.ComponentModel.DataAnnotations;

public class CreateAnimeRequestDto
{
    [Required]
    [MinLength(2, ErrorMessage = "Name must contain at least 2 characters")]
    [MaxLength (280, ErrorMessage = "Name can not be over 280 characters")]
    public string Name { get; set; } = string.Empty;

    public string Img { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    [Required]
    [MinLength(5, ErrorMessage = "Description must contain at least 5 characters")]
    [MaxLength (500, ErrorMessage = "Description can not be over 500 characters")]
    public string Description { get; set; } = string.Empty;
}