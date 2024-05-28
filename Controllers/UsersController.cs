using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    // In-memory dictionary to store user tokens
    private static readonly Dictionary<string, string> _userTokens = new Dictionary<string, string>();

    [HttpPost("save-token")]
    public IActionResult SaveToken([FromBody] SaveTokenModel model)
    {
        _userTokens[model.Username] = model.Token;
        return Ok();
    }

    [HttpPost("reply")]
    public async Task<IActionResult> ReplyToComment([FromBody] ReplyModel model)
    {
        if (_userTokens.TryGetValue(model.RepliedToUsername, out var userToken))
        {
            await NotificationService.SendNotificationAsync(userToken, "Nuevo comentario", $"{model.Author} ha respondido a tu comentario");
            return Ok();
        }
        return NotFound();
    }
}

public class SaveTokenModel
{
    public string Username { get; set; }
    public string Token { get; set; }
}
