using FirstCRUDController.Controllers.Rooms;

namespace FirstCRUDController.Controllers.Maids;

public class MaidsResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<RoomResponse> Rooms { get; set; }
}