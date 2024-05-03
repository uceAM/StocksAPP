﻿using Microsoft.AspNetCore.Mvc;
using StocksAPI.Dto.CommentDto;
using StocksAPI.Interfaces;
using StocksAPI.Mappers.CommentMappers;

namespace StocksAPI.Controllers;

[Route("api/comments")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IStockService _stockService;
    public CommentController(ICommentService commentService, IStockService stockService)
    {
        _commentService = commentService;
        _stockService = stockService;
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
    public async Task<IActionResult> AddComment([FromRoute]int stockId, [FromBody] CreateCommentDto newData)
    {
        if (newData == null || !ModelState.IsValid || stockId < 0)
        {
            return BadRequest();
        }
        var parentStock = await  _stockService.StockExists(stockId);
        if (!parentStock)
            return NotFound($"Stock with stockId {stockId} not found");
        var model = newData.ToComment(stockId);
        if (await _commentService.AddComment(model))
            return CreatedAtAction(nameof(GetCommentById), new { model.Id }, model.ToCommentDto());
        return BadRequest();
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
        var dbComment= await _commentService.GetComment(id);
        if(dbComment == null)
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