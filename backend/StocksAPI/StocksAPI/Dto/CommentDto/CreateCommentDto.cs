﻿namespace StocksAPI.Dto.CommentDto;

public class CreateCommentDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
