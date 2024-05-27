using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
    {
        var message = new Message()
        {
            Token = request.Token,
            Notification = new Notification()
            {
                Title = request.Title,
                Body = request.Body
            }
        };

        var result = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        return Ok(result);
    }
}

public class NotificationRequest
{
    public string Token { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}
