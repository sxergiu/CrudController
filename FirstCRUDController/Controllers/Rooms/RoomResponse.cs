namespace FirstCRUDController.Controllers.Rooms;

public class RoomResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool isOccupied { get; set; }
    public string Review { get; set; }
    public string MaidId { get; set; }
}