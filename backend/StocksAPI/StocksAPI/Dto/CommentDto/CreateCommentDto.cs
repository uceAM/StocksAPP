using System.ComponentModel.DataAnnotations;

namespace StocksAPI.Dto.CommentDto;

public class CreateCommentDto
{
    [Required]
    [MinLength(1,ErrorMessage="Title cannot be empty.")]
    [MaxLength(50,ErrorMessage="Title cannot be longer than 50 characters")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(5, ErrorMessage = "Content should atleast be 5 characters long.")]
    [MaxLength(255, ErrorMessage = "Title cannot be longer than 255 characters")]
    public string Content { get; set; } = string.Empty;
}
