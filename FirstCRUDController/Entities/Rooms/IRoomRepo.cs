namespace FirstCRUDController.Entities.Rooms;

public interface IRoomRepo
{
    Task<Room> GetRoomById(string id);
    Task<bool> IsReviewUnique(string review);
    Task<bool> DeleteRoom(string id);
    Task<IEnumerable<Room>> GetAllRoomsAsync();
    Task<Room> CreateRoom(Room r);
    void SaveAsync();
}