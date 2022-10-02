using Microsoft.AspNetCore.Mvc;
using NotificationTest.Requests;
using NotificationTest.Services;

namespace NotificationTest.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayersController : ControllerBase
{
   private readonly IPlayerService _playerService;
    public PlayersController(IPlayerService playerService)
    {
        _playerService = playerService;
    }
    [HttpGet("Receive/{playerId}")]
    public IActionResult GetReceiveGift([FromQuery] Guid playerId)
    {
        return Ok(_playerService.GetReceiveGift(playerId));
    }
    [HttpPost]
    public async Task<IActionResult> SendGiftAsync([FromBody] SendGiftRequest request)
    {
        await _playerService.SendGiftAsync(request);
        return Ok();
    }
    [HttpPut("Receive")]
    public async Task<IActionResult> ReceiveGiftAsync([FromBody] ReceiveGiftRequest request)
    {
        string? errorMessage = await _playerService.ReceiveGiftAsync(request);
        if(errorMessage != null) return BadRequest(errorMessage);
        return Ok();
    }
    [HttpPut("Reject")]
    public async Task<IActionResult> RejectGiftAsync([FromBody] RejectGiftRequest request)
    {
        string? errorMessage = await _playerService.RejectGiftAsync(request);
        if(errorMessage != null) return BadRequest(errorMessage);
        return Ok();
    }
}
