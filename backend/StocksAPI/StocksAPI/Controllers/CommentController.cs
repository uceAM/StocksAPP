using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StocksAPI.Dto.CommentDto;
using StocksAPI.Extensions;
using StocksAPI.Interfaces;
using StocksAPI.Mappers.CommentMappers;
using StocksAPI.Models;

namespace StocksAPI.Controllers;

[Route("api/comments")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IStockService _stockService;
    private readonly UserManager<WebUser> _userManager;
    public CommentController(ICommentService commentService, IStockService stockService, UserManager<WebUser> userManager)
    {
        _commentService = commentService;
        _stockService = stockService;
        _userManager = userManager;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllComments()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var listComment = await _commentService.GetAllComments();
        var data = listComment.Select(a => a.ToCommentDto());
        return Ok(data);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetCommentById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var data = await _commentService.GetComment(id);
        if (data == null)
        {
            return NotFound();
        }
        return Ok(data?.ToCommentDto());
    }

    [HttpPost("{stockId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> AddComment([FromRoute] int stockId, [FromBody] CreateCommentDto newData)
    {
        if (newData == null || !ModelState.IsValid || stockId < 0)
        {
            return BadRequest();
        }
        var parentStock = await _stockService.StockExists(stockId);
        if (!parentStock)
            return NotFound($"Stock with stockId {stockId} not found");
        var username = User.GetUsername();
        if (username == null)
        {
            var appUser = await _userManager.FindByNameAsync(username);
            var model = newData.ToComment(stockId);
            model.UserId = appUser.Id;
            if (await _commentService.AddComment(model))
                return CreatedAtAction(nameof(GetCommentById), new { model.Id }, model.ToCommentDto());
            return BadRequest();
        }
        return BadRequest("User not found");
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var dbComment = await _commentService.GetComment(id);
        if (dbComment == null)
            return NotFound($"Comment with id {id} does not exist");
        await _commentService.RemoveComment(dbComment);
        return Ok(dbComment.ToCommentDto());
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] CreateCommentDto updateData)
    {
        if (id < 0 || updateData == null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var dbComment = await _commentService.GetComment(id);
        if (dbComment == null)
            return NotFound();
        await _commentService.UpdateComment(dbComment, updateData);
        return NoContent();
    }
}
