using Microsoft.EntityFrameworkCore;
using StocksAPI.Data;
using StocksAPI.Dto.CommentDto;
using StocksAPI.Helpers;
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

    public async Task<List<Comment>> GetAllComments(CommentQueryObject options)
    {
        var query = _context.Comments.Include(a => a.User).AsQueryable();
        if (!string.IsNullOrEmpty(options.Symbol))
        {
            query = query.Where(c => c.Stock.Symbol == options.Symbol);
        }
        if (options.SortOrder)
        {
            query = query.OrderByDescending(a => a.CreatedOn);
        }
        return await query.ToListAsync();
    }

    public async Task<Comment?> GetComment(int id)
    {
        return await _context.Comments.Include(a => a.User).FirstOrDefaultAsync(c => c.Id == id);
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
