namespace FirstCRUDController.Controllers.Rooms;

public record RoomRequest(string Name, bool IsOccupied, string Review,string MaidId);
