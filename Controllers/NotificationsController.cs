using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly NotificationService _notificationService;

    public NotificationsController(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("reply")]
    public async Task<IActionResult> ReplyToComment([FromBody] ReplyModel model)
    {
        // Obtener el token de FCM del usuario al que se le está respondiendo
        var userToken = await GetUserTokenAsync(model.RepliedToUsername);

        // Enviar la notificación
        await NotificationService.SendNotificationAsync(userToken, "Nuevo comentario", $"{model.Author} ha respondido a tu comentario");

        return Ok();
    }

    private Task<string> GetUserTokenAsync(string username)
    {
        // Lógica para obtener el token de FCM del usuario desde la base de datos
        return Task.FromResult("user-fcm-token");
    }
}
