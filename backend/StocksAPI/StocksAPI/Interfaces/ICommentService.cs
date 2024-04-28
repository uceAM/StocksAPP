using StocksAPI.Dto.CommentDto;
using StocksAPI.Helpers;
using StocksAPI.Models;

namespace StocksAPI.Interfaces;

public interface ICommentService
{
    public Task<List<Comment>> GetAllComments();
    public Task<Comment?> GetComment(int id);
    public Task<bool> AddComment(Comment comment);
    public Task<Comment> RemoveComment(Comment comment);
    public Task<bool> UpdateComment(Comment comment, CreateCommentDto userComment);
}
