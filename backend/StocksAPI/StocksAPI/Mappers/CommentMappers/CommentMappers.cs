
using StocksAPI.Dto.CommentDto;
using StocksAPI.Dto.Comment;
using StocksAPI.Models;

namespace StocksAPI.Mappers.CommentMappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto()
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
            };
        }
        public static Comment ToComment(this CreateCommentDto userComment, int stockId)
        {
            return new Comment()
            {
                Content = userComment.Content,
                Title = userComment.Title,
                StockId = stockId,
            };
        }
    }
}
