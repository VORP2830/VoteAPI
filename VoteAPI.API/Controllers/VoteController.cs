using Microsoft.AspNetCore.Mvc;
using VoteAPI.Application.DTOs;
using VoteAPI.Application.Interfaces;
using VoteAPI.Domain.Exceptions;

namespace VoteAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;
        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(VoteDTO model)
        {
            try
            {
                var result = await _voteService.Create(model);
                return Ok(result);
            }
            catch (VoteAPIException ex)
            {
                var errorResponse = new
                {
                    Message = ex.Message
                };
                return BadRequest(errorResponse);
            }
            catch (Exception)
            {
                var errorResponse = new
                {
                    Message = "Erro ao votar"
                };
                return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _voteService.FindResult();
                return Ok();
            }
            catch (VoteAPIException ex)
            {
                var errorResponse = new
                {
                    Message = ex.Message
                };
                return BadRequest(errorResponse);
            }
            catch (Exception)
            {
                var errorResponse = new
                {
                    Message = "Erro ao votar"
                };
                return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}