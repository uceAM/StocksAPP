using Microsoft.EntityFrameworkCore;
using StocksAPI.Data;
using StocksAPI.Dto.CommentDto;
using StocksAPI.Interfaces;
using StocksAPI.Models;

namespace StocksAPI.Repositories;

public class CommentService : ICommentService
{
    public readonly ApplicationDbContext _context;

    public CommentService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> AddComment(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Comment>> GetAllComments()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetComment(int id)
    {
        return await _context.Comments.FindAsync(id);
    }

    public async Task<Comment> RemoveComment(Comment comment)
    {
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<bool> UpdateComment(Comment comment, CreateCommentDto userComment)
    {
        comment.Title = userComment.Title;
        comment.Content = userComment.Content;
        await _context.SaveChangesAsync();
        return true;
    }
}
